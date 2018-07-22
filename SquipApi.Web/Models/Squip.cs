using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace SquipApi.Models
{
    [DynamoDBTable("squips")]
    public class Squip
    {
        [DynamoDBHashKey("squipId")]
        public string Id { get; set; }

        [DynamoDBProperty("title")]
        public string Title { get; set; }

        [DynamoDBProperty("body")]
        public string Body { get; set; }
        
        [DynamoDBProperty("tags",typeof(StringListConverter))]
        public IList<string> Tags { get; set; }
    }
    public class StringListConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            return entry.AsListOfString();
        }

        public DynamoDBEntry ToEntry(object value)
        {
            return value as List<string>;
        }
    }
}
