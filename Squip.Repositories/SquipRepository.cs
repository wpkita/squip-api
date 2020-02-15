using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Squip.Domain;
using StackExchange.Redis;

namespace Squip.Data
{
    public class SquipRepository : ISquipRepository
    {
        private readonly IDatabase _redisDb;

        public SquipRepository(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect(config["RedisConnectionString"]);
            _redisDb = redis.GetDatabase();
        }

        public async Task<Idea> GetRandomIdea()
        {
            var randomIdeaId = await _redisDb.SetRandomMemberAsync("ideaIds");

            return null;
        }
    }
}
