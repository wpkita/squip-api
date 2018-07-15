using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using SquipApi.Models;

namespace SquipApi.Repositories
{
    public class CosmosDbSquipRepository : ISquipRepository
    {

        private readonly AmazonDynamoDBClient _dbClient;
        private readonly DynamoDBContext _dbContext;
        private const string tableName = "squips";

        public CosmosDbSquipRepository(IConfiguration configuration)
        {
            _dbClient = new AmazonDynamoDBClient();
            _dbContext = new DynamoDBContext(_dbClient);
        }
        public async Task<IEnumerable<Squip>> GetAll()
        {
            return await _dbContext.ScanAsync<Squip>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<Squip> GetById(string id)
        {
            return await _dbContext.LoadAsync<Squip>(id);
        }

        public async Task Add(Squip squip)
        {
            await _dbContext.SaveAsync(squip);
        }

        public async Task Remove(Squip squip)
        {
            await _dbContext.DeleteAsync(squip);
        }

        public async Task Update(Squip squip)
        {
            await _dbContext.SaveAsync(squip);
        }
    }
}
