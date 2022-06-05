using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Squip.Rest.Domain;
using StackExchange.Redis;

namespace Squip.Rest.Repositories
{
    public abstract class RedisRepository<T> : IRepository<T> where T : IDomainModel
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        protected readonly IDatabase RedisDb;

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

        public async Task<bool> DoesExistById(Guid id)
        {
            var doesExist = await RedisDb.KeyExistsAsync(EntityRedisKey(id));

            return doesExist;
        }

        public async Task<T> GetById(Guid id)
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
                var entity = await GetById(Guid.Parse(entityId));
                entities.Add(entity);
            }

            return entities;
        }

        public async Task<bool> Create(T entity)
        {
            entity.PreCreate();
            var entityJson = JsonConvert.SerializeObject(entity, _jsonSerializerSettings);

            await RedisDb.StringSetAsync(EntityRedisKey(entity.Id), entityJson);

            // Cache Id for random selection later
            await RedisDb.SetAddAsync(ActiveEntityIdsSetName, entity.Id.ToString());

            return true;
        }

        public async Task<bool> Update(T entity)
        {
            entity.PreUpdate();
            var entityJson = JsonConvert.SerializeObject(entity, _jsonSerializerSettings);

            await RedisDb.StringSetAsync(EntityRedisKey(entity.Id), entityJson);

            return true;
        }

        public async Task<bool> Archive(Guid id)
        {
            var didSucceed = await RedisDb.SetMoveAsync(
                ActiveEntityIdsSetName,
                ArchivedEntityIdsSetName,
                id.ToString()
            );

            return didSucceed;
        }

        private string EntityRedisKey(Guid id)
        {
            return $"{EntityName}:{id}";
        }
    }
}
