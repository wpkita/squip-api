using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squip.Rest.Domain;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers
{
    [ApiController]
    [Route("api/ideas")]
    public class IdeaController : ControllerBase
    {
        private readonly IRepository<Idea> _ideaRepository;
        private readonly IMapper _mapper;

        public IdeaController(IRepository<Idea> ideaRepository, IMapper mapper)
        {
            _ideaRepository = ideaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IEnumerable<IdeaDto>> GetIdeas()
        {
            var ideasFromRepo = await _ideaRepository.GetAll();

            var ideaDtos = ideasFromRepo.Select(IdeasProfile.MapIdeaToDto);

            return ideaDtos;
        }

        [HttpGet("{ideaId}", Name = "GetIdea")]
        public async Task<IActionResult> GetIdea(Guid ideaId)
        {
            var ideaFromRepo = await _ideaRepository.GetById(ideaId);

            if (ideaFromRepo == null)
                return NotFound();

            var ideaDto = IdeasProfile.MapIdeaToDto(ideaFromRepo);
            return Ok(ideaDto);
        }

        [HttpPost]
        public async Task<ActionResult<IdeaDto>> CreateIdea(IdeaForCreationDto idea)
        {
            var ideaEntity = _mapper.Map<Idea>(idea);
            await _ideaRepository.Create(ideaEntity);

            var ideaToReturn = _mapper.Map<IdeaDto>(ideaEntity);
            return CreatedAtRoute("GetIdea", new { ideaId = ideaToReturn.Id }, ideaToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIdea(Guid id, IdeaDto idea)
        {
            if (id != idea.Id)
            {
                return BadRequest();
            }

            var ideaEntity = _mapper.Map<Idea>(idea);

            await _ideaRepository.Update(ideaEntity);

            return NoContent();
        }

        [HttpDelete("{ideaId}")]
        public async Task<IActionResult> DeleteIdea(Guid ideaId)
        {
            if (!await _ideaRepository.DoesExistById(ideaId))
                return NotFound();

            await _ideaRepository.Archive(ideaId);

            return NoContent();
        }
    }
}
