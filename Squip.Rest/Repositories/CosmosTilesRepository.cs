using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public abstract class CosmosTilesRepository<T> : IRepository<T> where T : IDomainModel
    {
        readonly CosmosClient _client;

        protected CosmosTilesRepository(IConfiguration configuration)
        {
            _client = new CosmosClient(configuration["COSMOS_DB_CONN_STRING"]);
        }

        protected abstract string CollectionName { get; }

        public async Task<IEnumerable<Tile>> Get()
        {
            var myFirstTile = new Tile
            {
                Id = Guid.NewGuid().ToString(),
                Name = "My first tile",
                Type = "counter"
            };

            var container = _client.GetContainer("db-squip-dev", "tiles");
            await container.CreateItemAsync<Tile>(myFirstTile);

            var query = container.GetItemQueryIterator<Tile>();
            List<Tile> results = new List<Tile>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ReadNextAsync());
            }

            return results;
        }

        public async Task<bool> DoesExistById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<T> Create(T t)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Update(T t)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Archive(string id)
        {
            throw new NotImplementedException();
        }
    }
}
