using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Squip.Api.Dtos;
using Squip.Api.Secrets;
using StackExchange.Redis;

namespace Squip.Api.Repositories
{
    public class FirestoreSquipRepository : ISquipRepository
    {
        private const string SquipCollectionName = "squips";
        private const string PresentationCollectionName = "presentations";
        private const string ReactionCollectionName = "reactions";
        private const string IdeaCollectionName = "ideas";
        private readonly FirestoreDb firestoreDb;

        //"localhost:6379,password=rediskita");
        private readonly IDatabase redisDb;

        public FirestoreSquipRepository(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect(config["REDIS_CONNECTION_STRING"]);
            redisDb = redis.GetDatabase();
            firestoreDb = FirestoreDb.Create(config["FIREBASE_PROJECT_ID"]);
        }

        public async Task<IdeaSecret> GetSquip()
        {
            var randomId = await redisDb.SetRandomMemberAsync(SquipCollectionName);
            DocumentReference docRef = firestoreDb.Document($"{SquipCollectionName}/{randomId}");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            IdeaSecret squip = null;
            if (docSnap.Exists)
            {
                var docDict = docSnap.ToDictionary();
                squip = new IdeaSecret(docDict);
                squip.Id = docSnap.Id;
            }
            return squip;
        }

        public async Task<PresentationSecret> AddPresentation(PresentationSecret presentation)
        {
            var presDict = ObjectToDictionaryHelper.ToDictionary(presentation);
            DocumentReference documentReference = await firestoreDb.Collection("presentations").AddAsync(presDict);
            presentation.Id = documentReference.Id;
            return presentation;
        }

        public async Task<ReactionSecret> AddReaction(ReactionSecret reaction)
        {
            var reacDict = ObjectToDictionaryHelper.ToDictionary(reaction);
            DocumentReference documentReference = await firestoreDb.Collection("reactions").AddAsync(reacDict);
            reaction.Id = documentReference.Id;
            return reaction;
        }

        public async Task<IdeaSecret> AddIdea(IdeaSecret idea)
        {
            await firestoreDb.Collection(IdeaCollectionName).AddAsync(idea);
            var ideaRef = await redisDb.SetAddAsync(IdeaCollectionName, idea.Id);
            return new IdeaSecret(new Dictionary<string, object>());
        }
    }
}
