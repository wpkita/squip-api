using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SquipApi.Models;

namespace SquipApi.Repositories
{
    public class DynamoDbSquipRepository : ISquipRepository
    {

        public DynamoDbSquipRepository(IConfiguration configuration)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Squip>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Squip> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Add(Squip squip)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(Squip squip)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Squip squip)
        {
            throw new NotImplementedException();
        }
    }
}
