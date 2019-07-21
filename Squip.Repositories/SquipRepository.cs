using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using StackExchange.Redis;

namespace Squip.Repositories
{
    public class SquipRepository : ISquipRepository
    {
        private const string IdeaCollectionName = "ideas";
        private const string PresentationCollectionName = "presentations";
        private const string ReactionCollectionName = "reactions";
        private const string RejectCollectionName = "rejects";
        private readonly FirestoreDb firestoreDb;
        private readonly IDatabase redisDb;
        private readonly JsonSerializerSettings JsonSerializerSettings;

        public SquipRepository(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect(config["REDIS_CONNECTION_STRING"]);
            redisDb = redis.GetDatabase();
            firestoreDb = FirestoreDb.Create(config["GCP_PROJECT_ID"]);
            JsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        }

        public async Task<string> GetRandomIdeaId()
        {
            var randomIdeaId = await redisDb.SetRandomMemberAsync("ideaIds");

            return randomIdeaId;
        }

        public string GetNextIdeaId()
        {
            return firestoreDb.Collection(IdeaCollectionName).Document().Id;
        }

        public string GetNextPresentationId()
        {
            return firestoreDb.Collection(PresentationCollectionName).Document().Id;
        }

        public string GetNextReactionId()
        {
            return firestoreDb.Collection(ReactionCollectionName).Document().Id;
        }
    }
}
