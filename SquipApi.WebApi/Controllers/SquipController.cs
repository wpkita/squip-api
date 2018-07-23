using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquipApi.WebApi.Models;

namespace SquipApi.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class SquipController : Controller
    {
        private readonly SquipContext _context;
        private readonly IMapper _mapper;

        public SquipController(SquipContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            if (!_context.Squips.Any())
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
        public ActionResult<SquipDto> Get(long id)
        {
            var squip = _context.Squips.Include(s => s.SquipTags).ThenInclude(st => st.Tag)
                .SingleOrDefault(s => s.Id == id);
            if (squip == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<Squip, SquipDto>(squip);

            return dto;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Squip squip)
        {
            _context.Squips.Add(squip);
            _context.SaveChanges();

            return CreatedAtRoute("GetSquip", new {id = squip.Id}, squip);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]SquipDto squipFromRequest)
        {
            var squip = _context.Squips.Include(s => s.SquipTags).ThenInclude(st => st.Tag)
                .SingleOrDefault(s => s.Id == id);
            if (squip == null)
            {
                return NotFound();
            }

            squip.Title = squipFromRequest.Title;
            squip.Body = squipFromRequest.Body;
            var newTags = new HashSet<Tag>(squipFromRequest.Tags.Select(nt => _context.Tags.SingleOrDefault(t => t.Name == nt) ?? new Tag{Name=nt}));
            for (int i = squip.SquipTags.Count - 1; i >= 0; i--)
            {
                if (!newTags.Contains(squip.SquipTags[i].Tag))
                {
                    squip.SquipTags.RemoveAt(i);
                }
            }

            foreach (var newTag in newTags)
            {
                if (!squip.SquipTags.Select(x => x.Tag).Contains(newTag))
                {
                    squip.SquipTags.Add(new SquipTag
                    {
                        Tag = newTag
                    });
                }
            }

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
