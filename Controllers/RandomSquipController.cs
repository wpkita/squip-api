using Microsoft.AspNetCore.Mvc;
using Squip.Models;
using System;
using System.Linq;

namespace Squip.Api.Controllers
{
    [Route("/api/[controller]")]
    public class RandomSquipController : Controller
    {
        private readonly SquipContext _context;
        private static Random random = new Random();

        public RandomSquipController(SquipContext context)
        {
            _context = context;

            if (_context.Squips.Count() == 0)
            {
                _context.Squips.Add(new SquipModel { Body = "Body1"});
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            // This will not scale well
            var squips = _context.Squips.ToList();
            if (squips.Count == 0)
            {
                return NotFound();
            }

            var randomIndex = random.Next(squips.Count);

            return new ObjectResult(squips[randomIndex]);
        }
    }
}
