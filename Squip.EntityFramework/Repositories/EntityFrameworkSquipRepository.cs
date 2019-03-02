using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Squip.Pocos;
using Google.Cloud.Firestore;

namespace Squip.EntityFramework.Repositories
{
    public class EntityFrameworkSquipRepository : ISquipRepository
    {

        public async Task<IEnumerable<SquipPoco>> GetMostRecentSquipsAsync()
        {

            var db = FirestoreDb.Create("squip-183202");
            DocumentReference docRef = db.Collection("squips").Document("106jp9WkoLcPUytIUjFi");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            SquipPoco squip = null;
            if (docSnap.Exists)
            {
                var docDict = docSnap.ToDictionary();
                squip = new SquipPoco { Id = docSnap.Id, Title = docDict["title"].ToString(), Content = docDict["content"].ToString() };
            }
            return new List<SquipPoco>() { squip };
        }
    }
}
