using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public class CosmosTilesRepository
    {
        readonly CosmosClient _client;

        public CosmosTilesRepository(IConfiguration configuration)
        {
            _client = new CosmosClient(configuration["COSMOS_DB_CONN_STRING"]);
        }

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
    }
}
