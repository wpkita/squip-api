using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Squip.Api.DomainModels;
using StackExchange.Redis;

namespace Squip.Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(object id);
        Task<IEnumerable<T>> GetAll();
        Task Insert(T t);
        Task Update(T t);
    }

    public abstract class RedisRepository<T> : IRepository<T> where T : class
    {
        private readonly IDatabase redisDb;

        public RedisRepository(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect(config["REDIS_CONNECTION_STRING"]);
            redisDb = redis.GetDatabase();
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

        public async Task<T> GetById(object id)
        {
            T entity = default(T);

            try
            {
                var entityJson = await redisDb.StringGetAsync($"{entityName}:{id}");
                entity = JsonConvert.DeserializeObject<T>(entityJson);
            }
            catch
            {
                await redisDb.SetMoveAsync(entityName, archiveName, id.ToString());
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

        public Task Insert(T t)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(T t)
        {
            throw new System.NotImplementedException();
        }
    }

    public class IdeaRepository : RedisRepository<Idea>
        {
            public IdeaRepository(IConfiguration config) : base(config) { }
            protected override string entityName => "idea";
        }
}
