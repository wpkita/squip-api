using System.Linq;
using FluentAssertions;
using Squip.Rest.Domain;
using Squip.Rest.Repositories;
using Xunit;

namespace Squip.Rest.Tests.Repositories
{
    public class InMemoryRepositoryTests
    {
        [Fact]
        public async void Create_NotAddedThenAdd_CanGetById()
        {
            var unitUnderTest = new InMemoryRepository<Tile>();

            (await unitUnderTest.GetAll()).Count().Should().Be(0);

            var newTile = new Tile
            {
                Name = "My test tile",
                Type = "My type"
            };

            await unitUnderTest.Create(newTile);

            var tileFromRepo = await unitUnderTest.GetById(newTile.Id);

            tileFromRepo.Should().Be(newTile);
            tileFromRepo.Id.Should().Be(newTile.Id);
            tileFromRepo.Name.Should().Be(newTile.Name);
            tileFromRepo.Type.Should().Be(newTile.Type);

            (await unitUnderTest.GetAll()).Count().Should().Be(1);
        }

        [Fact]
        public async void Archive_NotAddedThenAdd_CanArchiveAndReturnsTrue()
        {
            var unitUnderTest = new InMemoryRepository<Tile>();

            (await unitUnderTest.GetAll()).Count().Should().Be(0);

            var newTile = new Tile
            {
                Name = "My test tile",
                Type = "My type"
            };

            await unitUnderTest.Create(newTile);

            (await unitUnderTest.GetAll()).Count().Should().Be(1);
            (await unitUnderTest.Archive(newTile.Id)).Should().Be(true);
            (await unitUnderTest.GetAll()).Count().Should().Be(0);
        }

        [Fact]
        public async void Archive_NotAdded_CannotArchiveAndReturnsFalse()
        {
            var unitUnderTest = new InMemoryRepository<Tile>();

            (await unitUnderTest.GetAll()).Count().Should().Be(0);
            (await unitUnderTest.Archive(string.Empty)).Should().Be(false);
            (await unitUnderTest.GetAll()).Count().Should().Be(0);
        }

        [Fact]
        public async void Update_NotAddedThenAddThenChangeProperty_PropertiesHaveChanged()
        {
            var unitUnderTest = new InMemoryRepository<Tile>();

            var oldName = "My test tile";
            var oldType = "My type";
            var updatedName = "My new updated name";
            var updatedType = "My new type";
            var newTile = new Tile
            {
                Name = oldName,
                Type = oldType
            };

            var didSucceed = await unitUnderTest.Create(newTile);
            didSucceed.Should().BeTrue();

            var tileFromRepo = await unitUnderTest.GetById(newTile.Id);
            tileFromRepo.Should().NotBeNull();
            tileFromRepo.Name = updatedName;
            tileFromRepo.Type = updatedType;

            didSucceed = await unitUnderTest.Update(tileFromRepo);
            didSucceed.Should().BeTrue();

            tileFromRepo = await unitUnderTest.GetById(newTile.Id);
            tileFromRepo.Name.Should().Be(updatedName);
            tileFromRepo.Type.Should().Be(updatedType);
        }

        [Fact]
        public async void DoesExistById_NotAddedThenAdd_ReturnsTrue()
        {
            var unitUnderTest = new InMemoryRepository<Tile>();

            (await unitUnderTest.GetAll()).Count().Should().Be(0);

            var newTile = new Tile
            {
                Name = "My test tile",
                Type = "My type"
            };

            await unitUnderTest.Create(newTile);

            var tileId = newTile.Id;

            (await unitUnderTest.DoesExistById(tileId)).Should().Be(true);
        }

        [Fact]
        public async void DoesExistById_NotAdded_ReturnsFalse()
        {
            var unitUnderTest = new InMemoryRepository<Tile>();

            (await unitUnderTest.GetAll()).Count().Should().Be(0);

            (await unitUnderTest.DoesExistById(string.Empty)).Should().Be(false);
        }

        [Fact]
        public async void GetById_DoesNotExist_ReturnsNull()
        {
            var unitUnderTest = new InMemoryRepository<Tile>();

            (await unitUnderTest.GetById(string.Empty)).Should().BeNull();
        }
    }
}
