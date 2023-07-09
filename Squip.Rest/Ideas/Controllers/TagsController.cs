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
public class TagsController : ControllerBase
{
    private readonly IRepository<Idea> _ideaRepository;

    public TagsController(IRepository<Idea> ideaRepository)
    {
        _ideaRepository = ideaRepository;
    }

    [HttpGet("{tagName}/ideas")]
    public async Task<IEnumerable<IdeaDto>> GetByTagAsync(string tagName, CancellationToken cancellationToken)
    {
        var ideasFromRepo = await _ideaRepository.GetByTagAsync(tagName, cancellationToken);

        var ideaDtos = ideasFromRepo.Select(IdeasProfile.MapIdeaToDto);

        return ideaDtos;
    }

}
