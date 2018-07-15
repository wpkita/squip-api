using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SquipApi.Models;
using SquipApi.Repositories;

namespace SquipApi.Controllers
{
    [Route("[controller]")]
    public class SquipController : Controller
    {
        private readonly ISquipRepository _squipRepository;
        public SquipController(ISquipRepository squipRepository)
        {
            _squipRepository = squipRepository;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Squip>> Get()
        {
            return await _squipRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Squip> Get(string id)
        {
            return await _squipRepository.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<Squip> Post([FromBody]Squip squip)
        {
            await _squipRepository.Add(squip);

            return squip;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Squip squip)
        {
            await _squipRepository.Update(squip);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            var squip = await _squipRepository.GetById(id);

            if (squip == null)
            {
                throw new Exception("Not Found");
            }

            await _squipRepository.Remove(squip);
        }
    }
}
