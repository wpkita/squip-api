using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Squip.Rest.Controllers;
using Squip.Rest.Domain;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;
using Xunit;

namespace Squip.Rest.Tests.Controllers
{
    public class TileControllerTests
    {
        private readonly IMapper _mockMapper;

        public TileControllerTests()
        {
            if (_mockMapper != null) return;

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new TilesProfile()); });

            _mockMapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void GetTiles_NoTilesAdded_GetsNoTiles()
        {
            var mockRepo = new Mock<IRepository<Tile>>();
            mockRepo.Setup(m => m.GetAll()).Returns(Task.FromResult(Enumerable.Empty<Tile>()));

            var tileController = new TileController(mockRepo.Object, _mockMapper);

            tileController.GetAll().Result.Should().BeEmpty();
        }

        [Fact]
        public async void GetTiles_OneTileAdded_GetsOneTile()
        {
            var mockRepo = new Mock<IRepository<Tile>>();
            mockRepo.Setup(m => m.GetAll()).Returns(Task.FromResult(new[]
            {
                new Tile
                {
                    Name = "Name",
                    Type = "Type"
                }
            }.AsEnumerable()));

            var tileController = new TileController(mockRepo.Object, _mockMapper);

            var tileDtos = await tileController.GetAll();
            tileDtos.Count().Should().Be(1);
        }

        [Fact]
        public async void GetTile_OneTileAddedWithMatchingId_GetsThatTile()
        {
            var idFromRepo = "a022bd9e-864a-430c-aa5a-62f40ab8dd4e";

            var fakeTile = new Tile
            {
                Id = idFromRepo,
                Name = "Name",
                Type = "Type"
            };

            var mockRepo = new Mock<IRepository<Tile>>();
            mockRepo.Setup(m => m.GetById(idFromRepo))
                .Returns(Task.FromResult(fakeTile));

            var tileController = new TileController(mockRepo.Object, _mockMapper);

            var result = await tileController.GetTile(idFromRepo);
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetTile_OneTileAddedButNoMatchingId_GetsNoTiles()
        {
            var idFromRepo = "a022bd9e-864a-430c-aa5a-62f40ab8dd4e";
            var idInRequest = "65cf0356-fa87-46ea-9af6-8ae8bae81379";

            var fakeTile = new Tile
            {
                Id = idFromRepo,
                Name = "Name",
                Type = "Type"
            };

            var mockRepo = new Mock<IRepository<Tile>>();
            mockRepo.Setup(m => m.GetById(idFromRepo))
                .Returns(Task.FromResult(fakeTile));

            var tileController = new TileController(mockRepo.Object, _mockMapper);

            var result = await tileController.GetTile(idInRequest);
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
