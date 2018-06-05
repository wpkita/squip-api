using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace SquipApi.Controllers
{
    public class Squip
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    [Route("[controller]")]
    public class SquipsController : Controller
    {
        private readonly string PrimaryKey;

        private readonly string EndpointUri;
        private const string DatabaseName = "SquipDb";
        private const string CollectionName = "SquipCollection";

        private readonly DocumentClient _documentClient;

        public SquipsController(IConfiguration configuration)
        {
            PrimaryKey = configuration["CosmosDbAccessKey"];
            EndpointUri = configuration["CosmosDbEndpointUri"];
            _documentClient = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Squip> Get()
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };

            // Here we find the Andersen family via its LastName
            var squips = _documentClient.CreateDocumentQuery<Squip>(
                UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), queryOptions);


            return squips;
        }

        [HttpGet("{id}")]
        public Squip Get(string id)
        {
            var queryOptions = new FeedOptions { MaxItemCount = 1 };

            var squip = _documentClient.CreateDocumentQuery<Squip>(
                UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName)).Where(x => x.Id == id).ToList();

            return squip.SingleOrDefault();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
