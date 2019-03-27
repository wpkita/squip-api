using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Squip.Api.Dtos;
using Squip.Api.Secrets;
using StackExchange.Redis;

namespace Squip.Api.Repositories
{
    public class FirestoreSquipRepository : ISquipRepository
    {
        private const string IdeaCollectionName = "ideas";
        private const string PresentationCollectionName = "presentations";
        private const string ReactionCollectionName = "reactions";
        private const string RejectCollectionName = "rejects";
        private readonly FirestoreDb firestoreDb;
        private readonly IDatabase redisDb;

        public FirestoreSquipRepository(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect(config["REDIS_CONNECTION_STRING"]);
            redisDb = redis.GetDatabase();
            firestoreDb = FirestoreDb.Create(config["FIREBASE_PROJECT_ID"]);
        }

        public async Task<IdeaSecret> GetIdea()
        {
            var randomIdeaId = await redisDb.SetRandomMemberAsync(IdeaCollectionName);
            DocumentReference docRef = firestoreDb.Document($"{IdeaCollectionName}/{randomIdeaId}");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();

            IdeaSecret idea = null;
            try
            {
                idea = docSnap.ConvertTo<IdeaSecret>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await redisDb.SetMoveAsync(IdeaCollectionName, RejectCollectionName, randomIdeaId);
            }

            return idea;
        }

        public async Task<PresentationSecret> AddPresentation(PresentationSecret presentation)
        {
            presentation.Id = firestoreDb.Collection(PresentationCollectionName).Document().Id;

            var presentationJson = JsonConvert.SerializeObject(presentation, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            await redisDb.ListRightPushAsync(PresentationCollectionName, presentationJson);
            return presentation;
        }

        public async Task<ReactionSecret> AddReaction(ReactionSecret reaction)
        {
            reaction.Id = firestoreDb.Collection(ReactionCollectionName).Document().Id;

            var reactionJson = JsonConvert.SerializeObject(reaction, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            await redisDb.ListRightPushAsync(ReactionCollectionName, reactionJson);
            return reaction;
        }

        public async Task<IdeaSecret> AddIdea(IdeaSecret idea)
        {
            idea.Id = firestoreDb.Collection(IdeaCollectionName).Document().Id;

            // Firestore
            await firestoreDb.Collection(IdeaCollectionName).Document(idea.Id).SetAsync(idea);

            // Redis
            var ideaRef = await redisDb.SetAddAsync(IdeaCollectionName, idea.Id);

            return idea;
        }
    }
}
