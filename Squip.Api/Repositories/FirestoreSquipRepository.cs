using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Squip.Api.Models;

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
        public async Task<SquipDto> GetSquip()
        {

            var index = r.Next(Ids.Count);
            var randomId = Ids[index];
            DocumentReference docRef = db.Document($"squips/{randomId}");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            SquipDto squip = null;
            if (docSnap.Exists)
            {
                var docDict = docSnap.ToDictionary();
                if (docDict.ContainsKey("content"))
                {
                    squip = new SquipDto { Content = docDict["content"].ToString() };
                }
                else
                {
                    // Delete the Squip if it does not have a content field. This is simple housekeeping
                    await docRef.DeleteAsync();
                    Ids.RemoveAt(index);
                }
            }
            return squip;
        }
    }
}
