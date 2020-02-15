using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Squip.Data
{
    public class SquipRepository : ISquipRepository
    {
        private const string IdeaCollectionName = "ideas";
        private const string PresentationCollectionName = "presentations";
        private const string ReactionCollectionName = "reactions";
        private readonly FirestoreDb _firestoreDb;
        private readonly IDatabase _redisDb;

        public SquipRepository(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect(config["RedisConnectionString"]);
            _redisDb = redis.GetDatabase();
            _firestoreDb = FirestoreDb.Create(config["GCP_PROJECT_ID"]);
        }

        public async Task<string> GetRandomIdeaId()
        {
            var randomIdeaId = await _redisDb.SetRandomMemberAsync("ideaIds");

            return randomIdeaId;
        }

        public string GetNextIdeaId()
        {
            return _firestoreDb.Collection(IdeaCollectionName).Document().Id;
        }

        public string GetNextPresentationId()
        {
            return _firestoreDb.Collection(PresentationCollectionName).Document().Id;
        }

        public string GetNextReactionId()
        {
            return _firestoreDb.Collection(ReactionCollectionName).Document().Id;
        }
    }
}
