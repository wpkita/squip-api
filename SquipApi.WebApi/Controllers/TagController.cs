using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SquipApi.EntityFramework;
using SquipApi.Pocos;
using SquipApi.WebApi.Dtos;

namespace SquipApi.WebApi.Controllers
{
    public class TagController : BaseController
    {
        private readonly SquipContext _context;
        private readonly IMapper _mapper;

        public TagController(SquipContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{name}")]
        public ActionResult<TagDto> GetByName(string name)
        {
            var tag = _context.Tags.Include(t => t.SquipTags).ThenInclude(st => st.Squip)
                .SingleOrDefault(t => t.Name == name);
            if (tag == null)
            {
                return NotFound();
            }

            return _mapper.Map<Tag, TagDto>(tag);
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
