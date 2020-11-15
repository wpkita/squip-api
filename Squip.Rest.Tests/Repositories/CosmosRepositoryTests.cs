using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Squip.Rest.Domain;
using Squip.Rest.Repositories;
using Xunit;

namespace Squip.Rest.Tests.Repositories
{
    public class CosmosRepositoryTests
    {
        private readonly IConfiguration _configuration;

        public CosmosRepositoryTests()
        {
            _configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
        }

        [Fact]
        public async void Create_NotAddedThenAdd_CanGetById()
        {
            var unitUnderTest = new TileCosmosRepository(_configuration);

            var newTile = new Tile
            {
                Name = "My test tile",
                Type = "My type"
            };

            await unitUnderTest.Create(newTile);

            var tileFromRepo = await unitUnderTest.GetById(newTile.Id);

            tileFromRepo.Id.Should().Be(newTile.Id);
            tileFromRepo.Name.Should().Be(newTile.Name);
            tileFromRepo.Type.Should().Be(newTile.Type);
        }

        [Fact]
        public async void Archive_NotAddedThenAdd_CanArchiveAndReturnsTrue()
        {
            var unitUnderTest = new TileCosmosRepository(_configuration);


            var newTile = new Tile
            {
                Name = "My test tile",
                Type = "My type"
            };

            await unitUnderTest.Create(newTile);

            (await unitUnderTest.Archive(newTile.Id)).Should().Be(true);
            (await unitUnderTest.GetById(newTile.Id)).Should().BeNull();
        }

        [Fact]
        public async void Archive_NotAdded_CannotArchiveAndReturnsFalse()
        {
            var unitUnderTest = new TileCosmosRepository(_configuration);

            var tileId = Guid.NewGuid().ToString();

            (await unitUnderTest.Archive(tileId)).Should().Be(false);
            (await unitUnderTest.GetById(tileId)).Should().BeNull();
        }

        [Fact]
        public async void Update_NotAddedThenAddThenChangeProperty_PropertiesHaveChanged()
        {
            var unitUnderTest = new TileCosmosRepository(_configuration);

            const string oldName = "My test tile";
            const string oldType = "My type";
            const string updatedName = "My new updated name";
            const string updatedType = "My new type";

            var newTile = new Tile
            {
                Name = oldName,
                Type = oldType
            };

            await unitUnderTest.Create(newTile);

            var tileFromRepo = await unitUnderTest.GetById(newTile.Id);
            tileFromRepo.Name = updatedName;
            tileFromRepo.Type = updatedType;

            await unitUnderTest.Update(tileFromRepo);

            tileFromRepo = await unitUnderTest.GetById(newTile.Id);
            tileFromRepo.Name.Should().Be(updatedName);
            tileFromRepo.Type.Should().Be(updatedType);
        }

        [Fact]
        public async void DoesExistById_NotAddedThenAdd_ReturnsTrue()
        {
            var unitUnderTest = new TileCosmosRepository(_configuration);

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
            var unitUnderTest = new TileCosmosRepository(_configuration);

            var tileId = Guid.NewGuid().ToString();

            (await unitUnderTest.DoesExistById(tileId)).Should().Be(false);
        }

        [Fact]
        public async void GetById_DoesNotExist_ReturnsNull()
        {
            var unitUnderTest = new TileCosmosRepository(_configuration);

            var tileId = Guid.NewGuid().ToString();

            (await unitUnderTest.GetById(tileId)).Should().BeNull();
        }
    }
}
