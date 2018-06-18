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
        public IEnumerable<Squip> Get()
        {
            return _squipRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Squip Get(string id)
        {
            return _squipRepository.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public Squip Post([FromBody]Squip squip)
        {
            _squipRepository.Add(squip);

            return squip;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Squip squip)
        {
            _squipRepository.Update(squip);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var squip = _squipRepository.GetById(id);

            if (squip == null)
            {
                throw new Exception("Not Found");
            }

            _squipRepository.Remove(squip);
        }
    }
}
