using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SquipApi.Models;
using SquipApi.Web.Models;

namespace SquipApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SquipController : Controller
    {
        private readonly SquipContext _context;
        public SquipController(SquipContext context)
        {
            _context = context;

            if (_context.Squips.Count() == 0)
            {
                _context.Squips.Add(new Squip() { Title = "My First Squip", Body = "Body goes here." });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Squip> Get()
        {
            return _context.Squips;
        }

        [HttpGet("{id}", Name = "GetSquip")]
        public ActionResult<Squip> Get(long id)
        {
            var squip = _context.Squips.Find(id);
            if (squip == null)
            {
                return NotFound();
            }

            return squip;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Squip squip)
        {
            _context.Squips.Add(squip);
            _context.SaveChanges();

            return CreatedAtRoute("GetSquip", new {id = squip.Id}, squip);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]Squip squipReq)
        {
            var squip = _context.Squips.Find(id);
            if (squip == null)
            {
                return NotFound();
            }

            squip.Title = squipReq.Title;
            squip.Body = squipReq.Body;

            _context.Squips.Update(squip);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var squip = _context.Squips.Find(id);
            if (squip == null)
            {
                return NotFound();
            }

            _context.Squips.Remove(squip);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
