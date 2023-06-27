using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squip.Rest.Ideas.Domain;
using Squip.Rest.Ideas.Dtos;
using Squip.Rest.Infrastructure.Common;

namespace Squip.Rest.Ideas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdeasController : ControllerBase
{
    private readonly IRepository<Idea> _ideaRepository;

    public IdeasController(IRepository<Idea> ideaRepository)
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

    [HttpGet("{id}", Name = "GetIdea")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var ideaFromRepo = await _ideaRepository.GetByIdAsync(id, cancellationToken);

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
        return CreatedAtRoute("GetIdea", new { id = ideaToReturn.Id }, ideaToReturn);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(
        Guid id,
        IdeaDto idea,
        CancellationToken cancellationToken
    )
    {
        if (id != idea.Id) return BadRequest();

        var ideaEntity = IdeasProfile.MapDtoToIdea(idea);

        await _ideaRepository.UpdateAsync(ideaEntity, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        if (!await _ideaRepository.DoesExistByIdAsync(id, cancellationToken))
            return NotFound();

        await _ideaRepository.ArchiveAsync(id, cancellationToken);

        return NoContent();
    }
}
