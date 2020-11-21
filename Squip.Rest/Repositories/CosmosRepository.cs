using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public class TileCosmosRepository : CosmosRepository<Tile>
    {
        public TileCosmosRepository(IConfiguration configuration, ILogger logger) : base(configuration, logger)
        {
        }

        protected override string ContainerName => "tiles";
    }

    public abstract class CosmosRepository<T> : IRepository<T> where T : IDomainModel
    {
        private readonly ILogger _logger;
        private readonly Container _container;

        protected CosmosRepository(IConfiguration configuration, ILogger logger)
        {
            _logger = logger;
            var cosmosOptions = new CosmosClientOptions
            {
                Serializer = new CosmosJsonDotNetSerializer(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb))
            };

            var client = new CosmosClient(configuration["COSMOS_DB_CONN_STRING"], cosmosOptions);
            _container = client.GetContainer(configuration["COSMOS_DB_NAME"], ContainerName);
        }

        protected abstract string ContainerName { get; }

        public async Task<bool> DoesExistById(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<T>(id, new PartitionKey(id));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch (CosmosException cosmosException) when (cosmosException.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogWarning(cosmosException, "DoesExistById did not find {id}", id);

                return false;
            }
            catch (CosmosException cosmosException)
            {
                _logger.LogError(cosmosException, "DoesExistById failed for {id}", id);
            }

            return false;
        }

        public async Task<T> GetById(string id)
        {
            T item = default;

            try
            {
                var response = await _container.ReadItemAsync<T>(id, new PartitionKey(id));
                item = response.Resource;
            }
            catch (CosmosException cosmosException) when (cosmosException.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogWarning(cosmosException, "GetById did not find {id}", id);
            }
            catch (CosmosException cosmosException)
            {
                _logger.LogError(cosmosException, "GetById failed for {id}", id);
            }

            return item;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var results = new List<T>();

            try
            {
                var query = _container.GetItemQueryIterator<T>();

                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ReadNextAsync());
                }
            }
            catch (CosmosException cosmosException)
            {
                _logger.LogError(cosmosException, "GetAll failed");
            }

            return results;
        }

        public async Task<bool> Create(T t)
        {
            t.PreCreate();

            try
            {
                await _container.CreateItemAsync(t);

                return true;
            }
            catch (CosmosException cosmosException)
            {
                _logger.LogError(cosmosException, "Create failed for {item}", t);
            }

            return false;
        }

        public async Task<bool> Update(T t)
        {
            t.PreUpdate();

            try
            {
                await _container.UpsertItemAsync(t);

                return true;
            }
            catch (CosmosException cosmosException)
            {
                _logger.LogError(cosmosException, "Update failed for {item}", t);
            }

            return false;
        }

        public async Task<bool> Archive(string id)
        {
            try
            {
                await _container.DeleteItemAsync<T>(id, new PartitionKey(id));

                return true;
            }

            catch (CosmosException cosmosException) when (cosmosException.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogWarning(cosmosException, "Archive did not find {id}", id);
            }
            catch (CosmosException cosmosException)
            {
                _logger.LogError(cosmosException, "Archive failed for {id}", id);
            }

            return false;
        }
    }
}
