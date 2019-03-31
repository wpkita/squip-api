using System;
using Google.Cloud.Firestore;
using NodaTime;
using Squip.Api.DomainModels;

namespace Squip.Api.Repositories
{
    [FirestoreData]
    public class IdeaDbModel
    {

        [FirestoreProperty("content")]
        public string Content { get; set; }

        [FirestoreProperty("tags")]
        public string[] Tags { get; set; }

        [FirestoreProperty("userId")]
        public string UserId { get; set; }

        [FirestoreProperty("instantCreatedAt")]
        public DateTime InstantCreatedAt { get; set; }
    }
}
