using System;
using System.Collections.Generic;
using System.Linq;
using Google.Cloud.Firestore;

namespace Squip.Api.Secrets
{
    [FirestoreData]
    public class IdeaSecret
    {
        public string Id { get; set; }

        [FirestoreProperty("content")]
        public string Content { get; set; }

        [FirestoreProperty("tags")]
        public string[] Tags { get; set; }

        [FirestoreProperty("userId")]
        public string UserId { get; set; }
    }
}
