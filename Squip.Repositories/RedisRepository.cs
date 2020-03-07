using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Squip.Domain;
using StackExchange.Redis;

namespace Squip.Data
{
    public abstract class RedisRepository<T> : IRepository<T> where T : IDomainModel
    {
        protected readonly IDatabase RedisDb;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        protected RedisRepository(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect(config["RedisConnectionString"]);
            RedisDb = redis.GetDatabase();

            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        }

        protected abstract string EntityName { get; }

        private string ArchivedEntityIdsSetName => $"{EntityName}IdsArchived";

        protected string ActiveEntityIdsSetName => $"{EntityName}Ids";

        private string EntityRedisKey(string id)
        {
            return $"{EntityName}:{id}";
        }

        public async Task<bool> DoesExistById(string id)
        {
            var doesExist = await RedisDb.KeyExistsAsync(EntityRedisKey(id));

            return doesExist;
        }

        public async Task<T> GetById(string id)
        {
            var entity = default(T);

            try
            {
                var entityJson = await RedisDb.StringGetAsync(EntityRedisKey(id));
                entity = JsonConvert.DeserializeObject<T>(entityJson, _jsonSerializerSettings);
            }
            catch
            {
                // This is technically business logic? Hmm
                await Archive(id);
            }

            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            ICollection<T> entities = new Collection<T>();

            var entityIds = await RedisDb.SetMembersAsync(ActiveEntityIdsSetName);
            foreach (var entityId in entityIds)
            {
                var entity = await GetById(entityId);
                entities.Add(entity);
            }

            return entities;
        }

        public async Task<T> Create(T entity)
        {
            entity.PreCreate();
            var entityJson = JsonConvert.SerializeObject(entity, _jsonSerializerSettings);

            await RedisDb.StringSetAsync(EntityRedisKey(entity.Id), entityJson);

            // Cache Id for random selection later
            await RedisDb.SetAddAsync(ActiveEntityIdsSetName, entity.Id);

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            entity.PreUpdate();
            var entityJson = JsonConvert.SerializeObject(entity, _jsonSerializerSettings);

            await RedisDb.StringSetAsync(EntityRedisKey(entity.Id), entityJson);

            return entity;
        }

        public async Task<bool> Archive(string id)
        {
            var didSucceed = await RedisDb.SetMoveAsync(ActiveEntityIdsSetName, ArchivedEntityIdsSetName, id);

            return didSucceed;
        }
    }
}
