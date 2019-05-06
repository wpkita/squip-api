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
    public interface IRepository<T> where T : IDomainModel
    {
        Task<T> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task Insert(T t);
    }

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

        private string archiveName
        {
            get
            {
                return $"{entityName}Archive";
            }
        }

        private string setName
        {
            get
            {
                return $"{entityName}Ids";
            }
        }

        public async Task<T> GetById(string id)
        {
            T entity = default(T);

            try
            {
                var entityJson = await redisDb.StringGetAsync($"{entityName}:{id}");
                entity = JsonConvert.DeserializeObject<T>(entityJson, JsonSerializerSettings);
            }
            catch
            {
                await redisDb.SetMoveAsync(setName, archiveName, id);
            }

            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            ICollection<T> entities = new Collection<T>();

            var entityIds = await redisDb.SetMembersAsync(setName);
            foreach (var entityId in entityIds)
            {
                var entity = await GetById(entityId);
                entities.Add(entity);
            }

            return entities;
        }

        public async Task Insert(T t)
        {
            t.PreCreate();
            var entityJson = JsonConvert.SerializeObject(t, JsonSerializerSettings);

            // Save entity
            await redisDb.StringSetAsync($"{entityName}:{t.Id}", entityJson);

            await redisDb.SetAddAsync(setName, t.Id);
        }
    }

    public class IdeaRepository : RedisRepository<Idea>
        {
            public IdeaRepository(IConfiguration config) : base(config) { }
            protected override string entityName => "idea";
        }
}
