using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Squip.Models;
using System.Linq;

namespace Squip.Api.Controllers
{
    [Route("/api/[controller]")]
    public class SquipController : Controller
    {
        private readonly SquipContext _context;

        public SquipController(SquipContext context)
        {
            _context = context;

            if (_context.Squips.Count() == 0)
            {
                _context.Squips.Add(new SquipModel { Body = "Body1"});
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<SquipModel> GetAll()
        {
            return _context.Squips.ToList();
        }

        [HttpGet("{id}", Name = "GetSquip")]
        public IActionResult GetById(int id)
        {
            var squip = _context.Squips.FirstOrDefault(s => s.Id == id);
            if (squip == null)
            {
                return NotFound();
            }
            return new ObjectResult(squip);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SquipModel squip)
        {
            if (squip == null)
            {
                return BadRequest();
            }

            _context.Squips.Add(squip);
            _context.SaveChanges();

            return CreatedAtRoute("GetSquip", new { Id = squip.Id}, squip);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SquipModel proposedSquip)
        {
            if (proposedSquip == null || proposedSquip.Id != id)
            {
                return BadRequest();
            }

            var actualSquip = _context.Squips.FirstOrDefault(s => s.Id == id);
            if (actualSquip == null)
            {
                return NotFound();
            }

            actualSquip.Body = proposedSquip.Body;

            _context.Squips.Update(actualSquip);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var squipToDelete = _context.Squips.FirstOrDefault(s => s.Id == id);
            if (squipToDelete == null)
            {
                return NotFound();
            }

            _context.Squips.Remove(squipToDelete);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}
