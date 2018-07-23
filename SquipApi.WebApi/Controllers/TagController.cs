using Microsoft.AspNetCore.Mvc;
using SquipApi.WebApi.Models;
using System.Collections.Generic;

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

        [HttpGet("{id}")]
        public ActionResult<Tag> GetByName(string name)
        {
            var tag = _context.Tags.Find(name);
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
