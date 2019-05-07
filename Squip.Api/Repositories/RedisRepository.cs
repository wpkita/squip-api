using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Squip.Api.DomainModels;
using StackExchange.Redis;

namespace Squip.Api.Repositories
{
    public abstract class RedisRepository<T> : IRepository<T> where T : IDomainModel
    {
        private readonly IDatabase redisDb;
        private readonly JsonSerializerSettings JsonSerializerSettings;

        public RedisRepository(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect(config["REDIS_CONNECTION_STRING"]);
            redisDb = redis.GetDatabase();

            JsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        }

        protected abstract string entityName { get; }

        private string archivedEntityIdsSetName
        {
            get
            {
                return $"{entityName}IdsArchived";
            }
        }

        private string activeEntityIdsSetName
        {
            get
            {
                return $"{entityName}Ids";
            }
        }

        private string entityRedisKey(string id)
        {
            return $"{entityName}:{id}";
        }

        public async Task<bool> DoesExistById(string id)
        {
            var doesExist = await redisDb.KeyExistsAsync(entityRedisKey(id));

            return doesExist;
        }

        public async Task<T> GetById(string id)
        {
            T entity = default(T);

            try
            {
                var entityJson = await redisDb.StringGetAsync(entityRedisKey(id));
                entity = JsonConvert.DeserializeObject<T>(entityJson, JsonSerializerSettings);
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

            var entityIds = await redisDb.SetMembersAsync(activeEntityIdsSetName);
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
            var entityJson = JsonConvert.SerializeObject(entity, JsonSerializerSettings);

            // Save entity
            await redisDb.StringSetAsync(entityRedisKey(entity.Id), entityJson);

            // Cache Id for random selection later
            await redisDb.SetAddAsync(activeEntityIdsSetName, entity.Id);

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            entity.PreUpdate();
            var entityJson = JsonConvert.SerializeObject(entity, JsonSerializerSettings);

            // Save entity
            await redisDb.StringSetAsync(entityRedisKey(entity.Id), entityJson);

            return entity;
        }

        public async Task<bool> Archive(string id)
        {
            var didSucceed = await redisDb.SetMoveAsync(activeEntityIdsSetName, archivedEntityIdsSetName, id);

            return didSucceed;
        }
    }
}
