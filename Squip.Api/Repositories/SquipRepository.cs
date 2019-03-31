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
using Squip.Api.DomainModels;
using StackExchange.Redis;
using AutoMapper;
using NodaTime.Serialization.JsonNet;
using NodaTime;

namespace Squip.Api.Repositories
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
            firestoreDb = FirestoreDb.Create(config["FIREBASE_PROJECT_ID"]);
            JsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        }

        public async Task<string> GetRandomIdeaId()
        {
            var randomIdeaId = await redisDb.SetRandomMemberAsync(IdeaCollectionName);

            return randomIdeaId;
        }

        public string GetNextPresentationId()
        {
            return firestoreDb.Collection(PresentationCollectionName).Document().Id;
        }

        public string GetNextReactionId()
        {
            return firestoreDb.Collection(ReactionCollectionName).Document().Id;
        }

        public async Task<Idea> CreateIdea(Idea idea)
        {
            // Save idea to Firestore
            var ideaDbModel = Mapper.Map<IdeaDbModel>(idea);
            await firestoreDb.Collection(IdeaCollectionName).Document(idea.Id).SetAsync(ideaDbModel);

            // Save id to a Redis set so it can later be randomly chosen
            await redisDb.SetAddAsync(IdeaCollectionName, idea.Id);

            return idea;
        }

        public async Task<Idea> GetIdea(string id)
        {
            var ideaDocRef = firestoreDb.Document($"{IdeaCollectionName}/{id}");
            var ideaDocSnapshot = await ideaDocRef.GetSnapshotAsync();

            Idea idea = null;

            try
            {
                var ideaDbModel = ideaDocSnapshot.ConvertTo<IdeaDbModel>();
                idea = Mapper.Map<Idea>(ideaDbModel);
                idea.Id = ideaDocSnapshot.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await redisDb.SetMoveAsync(IdeaCollectionName, RejectCollectionName, id);
            }

            return idea;
        }

        public async Task<Presentation> CreatePresentation(Presentation presentation)
        {
            var presentationDbModel = Mapper.Map<PresentationDbModel>(presentation);

            // Serialize presentation to JSON string
            var presentationJson = JsonConvert.SerializeObject(presentationDbModel, JsonSerializerSettings);

            // Save presentation to Redis list for later processing
            await redisDb.ListRightPushAsync(PresentationCollectionName, presentationJson);

            return presentation;
        }

        public async Task<Reaction> CreateReaction(Reaction reaction)
        {
            // Serialize reaction to JSON string
            var reactionJson = JsonConvert.SerializeObject(reaction, JsonSerializerSettings);

            // Save reaction to Redis list for later processing
            await redisDb.ListRightPushAsync(ReactionCollectionName, reactionJson);

            return reaction;
        }
    }
}
