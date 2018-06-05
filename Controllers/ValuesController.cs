using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace squip_dotnet_api.Controllers
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

        public SquipsController(IConfiguration configuration)
        {
            PrimaryKey = configuration["CosmosDbAccessKey"];
            EndpointUri = configuration["CosmosDbEndpointUri"];
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Squip> Get()
        {

            var documentClient = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            // Set some common query options
            var queryOptions = new FeedOptions { MaxItemCount = -1 };

            // Here we find the Andersen family via its LastName
            var squips = documentClient.CreateDocumentQuery<Squip>(
                UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), queryOptions);

            return squips.ToList();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
