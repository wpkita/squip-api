using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquipApi.WebApi.Dtos;
using System.Collections.Generic;
using System.Linq;
using SquipApi.EntityFramework;
using SquipApi.Pocos;

namespace SquipApi.WebApi.Controllers
{
    public class SquipController : BaseController
    {
        private readonly SquipContext _context;
        private readonly IMapper _mapper;

        public SquipController(SquipContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<SquipDto> Get()
        {
            var squips = _context.Squips.Include(s => s.SquipTags).ThenInclude(st => st.Tag).ToList();

            return _mapper.Map<IEnumerable<Squip>, IEnumerable<SquipDto>>(squips);
        }

        [HttpGet("random")]
        public ActionResult<SquipDto> GetRandomSquip()
        {
            var squip = _context.Squips.OrderBy(s => Guid.NewGuid())
                .Include(s => s.SquipTags).ThenInclude(st => st.Tag).FirstOrDefault();
            if (squip == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<Squip, SquipDto>(squip);

            return dto;
        }

        [HttpGet("{id}", Name = "GetSquip")]
        public ActionResult<SquipDto> GetById(long id)
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
            var squip = new Squip {SquipTags = new List<SquipTag>()};

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
