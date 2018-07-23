using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using SquipApi.WebApi.Models;


namespace SquipApi.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TagController : Controller
    {
        private readonly SquipContext _context;

        public TagController(SquipContext context)
        {
            _context = context;

            if (!_context.Tags.Any())
            {
                _context.Tags.Add(new Tag {Name = "azure"});
                _context.SaveChanges();
            }
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

            return CreatedAtRoute("GetTag", new {name = tag.Name}, tag);
        }
    }
}
