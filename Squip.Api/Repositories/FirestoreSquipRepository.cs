using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Squip.Api.Dtos;
using Squip.Api.Secrets;

namespace Squip.Api.Repositories
{
    public class FirestoreSquipRepository : ISquipRepository
    {
        static Random r;
        static IList<string> Ids;
        static FirestoreDb db;

        static FirestoreSquipRepository()
        {
            r = new Random();
            db = FirestoreDb.Create("squip-183202");
            CollectionReference colRef = db.Collection("squips");
            QuerySnapshot querySnapshot = colRef.GetSnapshotAsync().Result;
            Ids = querySnapshot.Documents.Select(d => d.Id).ToList();
        }

        public async Task<SquipSecret> GetSquip()
        {
            var index = r.Next(Ids.Count);
            var randomId = Ids[index];
            DocumentReference docRef = db.Document($"squips/{randomId}");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            SquipSecret squip = null;
            if (docSnap.Exists)
            {
                var docDict = docSnap.ToDictionary();
                squip = new SquipSecret(docDict);
                squip.Id = docSnap.Id;
            }
            return squip;
        }

        public async Task<PresentationSecret> AddPresentation(PresentationSecret presentation)
        {
            var presDict = ObjectToDictionaryHelper.ToDictionary(presentation);
            DocumentReference documentReference = await db.Collection("presentations").AddAsync(presDict);
            presentation.Id = documentReference.Id;
            return presentation;
        }

        public async Task<ReactionSecret> AddReaction(ReactionSecret reaction)
        {
            var reacDict = ObjectToDictionaryHelper.ToDictionary(reaction);
            DocumentReference documentReference = await db.Collection("reactions").AddAsync(reacDict);
            reaction.Id = documentReference.Id;
            return reaction;
        }

        public async Task<bool> DoesPresentationExist(string id)
        {
            DocumentReference docRef = db.Document($"presentations/{id}");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();

            return docSnap.Exists;
        }
    }
}
