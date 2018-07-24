using Microsoft.AspNetCore.Mvc;
using SquipApi.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using SquipApi.Pocos;

namespace SquipApi.WebApi.Controllers
{
    public class TagController : BaseController
    {
        private readonly SquipContext _context;

        public TagController(SquipContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Tag> Get()
        {
            return _context.Tags;
        }

        [HttpGet("{name}")]
        public ActionResult<Tag> GetByName(string name)
        {
            var tag = _context.Tags.SingleOrDefault(t => t.Name == name);
            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();

            return CreatedAtRoute("GetTag", new { name = tag.Name }, tag);
        }
    }
}
