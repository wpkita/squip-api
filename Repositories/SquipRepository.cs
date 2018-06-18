using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using SquipApi.Models;

namespace SquipApi.Repositories
{
    public class CosmosDbSquipRepository : ISquipRepository
    {
        private readonly string PrimaryKey;

        private readonly string EndpointUri;
        private const string DatabaseName = "SquipDb";
        private const string CollectionName = "SquipCollection";

        private readonly DocumentClient _documentClient;
        public CosmosDbSquipRepository(IConfiguration configuration)
        {
            PrimaryKey = configuration["CosmosDbAccessKey"];
            EndpointUri = configuration["CosmosDbEndpointUri"];
            _documentClient = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
        }
        public IEnumerable<Squip> GetAll()
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };

            // Here we find the Andersen family via its LastName
            var squips = _documentClient.CreateDocumentQuery<Squip>(
                UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), queryOptions);


            return squips;
        }

        public Squip GetById(string id)
        {
            var queryOptions = new FeedOptions { MaxItemCount = 1 };

            var squip = _documentClient.CreateDocumentQuery<Squip>(
                UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName)).Where(x => x.Id == id).ToList();

            return squip.SingleOrDefault();
        }

        public void Add(Squip squip)
        {
            var result = _documentClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), squip);
        }

        public void Remove(Squip squip)
        {
            var result = _documentClient.DeleteDocumentAsync(
                UriFactory.CreateDocumentUri(DatabaseName, CollectionName, squip.Id));

            Console.Write("");
        }

        public void Update(Squip squip)
        {
            _documentClient.ReplaceDocumentAsync(
                UriFactory.CreateDocumentUri(DatabaseName, CollectionName, squip.Id), squip);
        }
    }
}
