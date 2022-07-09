using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public IdeaController(IRepository<Idea> ideaRepository)
        {
            _ideaRepository = ideaRepository;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IEnumerable<IdeaDto>> GetAsync(CancellationToken cancellationToken)
        {
            var ideasFromRepo = await _ideaRepository.GetAllAsync(cancellationToken);

            var ideaDtos = ideasFromRepo.Select(IdeasProfile.MapIdeaToDto);

            return ideaDtos;
        }

        [HttpGet("{ideaId}", Name = "GetIdea")]
        public async Task<IActionResult> GetAsync(Guid ideaId, CancellationToken cancellationToken)
        {
            var ideaFromRepo = await _ideaRepository.GetByIdAsync(ideaId, cancellationToken);

            if (ideaFromRepo == null)
                return NotFound();

            var ideaDto = IdeasProfile.MapIdeaToDto(ideaFromRepo);
            return Ok(ideaDto);
        }

        [HttpPost]
        public async Task<ActionResult<IdeaDto>> CreateAsync(
            IdeaForCreationDto idea,
            CancellationToken cancellationToken
        )
        {
            var ideaEntity = IdeasProfile.MapDtoToIdea(idea);
            await _ideaRepository.CreateAsync(ideaEntity, cancellationToken);

            var ideaToReturn = IdeasProfile.MapIdeaToDto(ideaEntity);
            return CreatedAtRoute("GetIdea", new { ideaId = ideaToReturn.Id }, ideaToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(
            Guid id,
            IdeaDto idea,
            CancellationToken cancellationToken
        )
        {
            if (id != idea.Id)
            {
                return BadRequest();
            }

            var ideaEntity = IdeasProfile.MapDtoToIdea(idea);

            await _ideaRepository.UpdateAsync(ideaEntity, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{ideaId}")]
        public async Task<IActionResult> DeleteAsync(
            Guid ideaId,
            CancellationToken cancellationToken
        )
        {
            if (!await _ideaRepository.DoesExistByIdAsync(ideaId, cancellationToken))
                return NotFound();

            await _ideaRepository.ArchiveAsync(ideaId, cancellationToken);

            return NoContent();
        }
    }
}
