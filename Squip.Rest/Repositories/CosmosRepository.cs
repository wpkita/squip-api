using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public class TileCosmosRepository : CosmosRepository<Tile>
    {
        public TileCosmosRepository(IConfiguration configuration) : base(configuration)
        {
        }

        protected override string ContainerName => "tiles";
    }

    public abstract class CosmosRepository<T> : IRepository<T> where T : IDomainModel
    {
        private readonly CosmosClient _client;
        private readonly Container _container;
        private readonly IMapper _mapper;

        protected CosmosRepository(IConfiguration configuration)
        {
            var cosmosOptions = new CosmosClientOptions
            {
                Serializer = new CosmosJsonDotNetSerializer(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb))
            };

            _client = new CosmosClient(configuration["COSMOS_DB_CONN_STRING"], cosmosOptions);
            _container = _client.GetContainer(configuration["COSMOS_DB_NAME"], ContainerName);
        }

        protected abstract string ContainerName { get; }

        public async Task<bool> DoesExistById(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<T>(id, new PartitionKey(id));

                if (response.StatusCode == HttpStatusCode.OK) return true;
            }
            catch (CosmosException cosmosException) when (cosmosException.StatusCode == HttpStatusCode.NotFound)
            {
                // Log it.

                if (cosmosException.StatusCode == HttpStatusCode.NotFound) return false;
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
            catch (CosmosException cosmosException)
            {
                // Log it.
            }

            return item;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var query = _container.GetItemQueryIterator<T>();

            var results = new List<T>();
            while (query.HasMoreResults) results.AddRange(await query.ReadNextAsync());

            return results;
        }

        public async Task<T> Create(T t)
        {
            t.PreCreate();

            var itemResponse = await _container.CreateItemAsync(t);

            return t;
        }

        public async Task<T> Update(T t)
        {
            t.PreUpdate();

            var itemResponse = await _container.UpsertItemAsync(t);

            return t;
        }

        public async Task<bool> Archive(string id)
        {
            try
            {
                await _container.DeleteItemAsync<T>(id, new PartitionKey(id));

                return true;
            }
            catch (CosmosException cosmosException)
            {
                if (cosmosException.StatusCode == HttpStatusCode.NotFound) return false;
            }

            return false;
        }
    }
}
