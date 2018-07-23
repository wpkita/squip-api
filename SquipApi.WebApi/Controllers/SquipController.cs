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
        public IEnumerable<SquipDto> Get()
        {
            var squips = _context.Squips.Include(s => s.SquipTags).ThenInclude(st => st.Tag).ToList();

            return _mapper.Map<IEnumerable<Squip>, IEnumerable<SquipDto>>(squips);
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
        public IActionResult Create([FromBody]SquipDto squipFromRequest)
        {
            var squip = new Squip();

            CopySquipInfo(squipFromRequest, squip);
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

            CopySquipInfo(squipFromRequest, squip);

            _context.Squips.Update(squip);
            _context.SaveChanges();

            return NoContent();
        }

        private void CopySquipInfo(SquipDto fromSquip, Squip toSquip)
        {
            toSquip.Title = fromSquip.Title;
            toSquip.Body = fromSquip.Body;

            var newTags = new HashSet<Tag>(fromSquip.Tags.Select(nt =>
                _context.Tags.SingleOrDefault(t => t.Name == nt) ?? new Tag {Name = nt}));
            for (var i = toSquip.SquipTags.Count - 1; i >= 0; i--)
            {
                if (!newTags.Contains(toSquip.SquipTags[i].Tag))
                {
                    toSquip.SquipTags.RemoveAt(i);
                }
            }

            foreach (var newTag in newTags)
            {
                if (!toSquip.SquipTags.Select(x => x.Tag).Contains(newTag))
                {
                    toSquip.SquipTags.Add(new SquipTag
                    {
                        Tag = newTag
                    });
                }
            }
        }

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
