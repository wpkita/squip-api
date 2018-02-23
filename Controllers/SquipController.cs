using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Squip.Models;
using System.Linq;

namespace Squip.Api.Controllers
{
    [Route("api/[controller]")]
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
    }
}
