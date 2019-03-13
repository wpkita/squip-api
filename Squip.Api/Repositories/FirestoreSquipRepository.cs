using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Squip.Api.Models;

namespace Squip.Api.Repositories
{
    public class FirestoreSquipRepository : ISquipRepository
    {
        static Random r = new Random();
        public async Task<SquipDto> GetSquip()
        {
            var db = await FirestoreDb.CreateAsync("squip-183202");
            CollectionReference colRef = db.Collection("squips");
            QuerySnapshot querySnapshot = await colRef.GetSnapshotAsync();
            var ids = querySnapshot.Documents.Select(d => d.Id).ToList();
            var index = r.Next(ids.Count);
            var randomId = ids[index];
            DocumentReference docRef = colRef.Document(randomId);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            SquipDto squip = null;
            if (docSnap.Exists)
            {
                var docDict = docSnap.ToDictionary();
                if (docDict.ContainsKey("content"))
                {
                    squip = new SquipDto { Content = docDict["content"].ToString() };
                }
            }
            return squip;
        }
    }
}
