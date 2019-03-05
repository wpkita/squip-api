using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Squip.Api.Models;

namespace Squip.Api.Repositories
{
    public class FirestoreSquipRepository : ISquipRepository
    {

        public async Task<SquipDto> GetSquip()
        {

            var db = await FirestoreDb.CreateAsync("squip-183202");
            DocumentReference docRef = db.Document("squips/106jp9WkoLcPUytIUjFi");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            SquipDto squip = null;
            if (docSnap.Exists)
            {
                var docDict = docSnap.ToDictionary();
                squip = new SquipDto { Content = docDict["content"].ToString() };
            }
            return squip;
        }
    }
}
