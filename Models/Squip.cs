using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;

namespace SquipApi.Models
{
    [DynamoDBTable("Squips")]
    public class Squip
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
