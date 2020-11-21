using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squip.Rest.Domain;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkIdeasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Idea> _squipRepository;

        public BulkIdeasController(IRepository<Idea> squipRepository, IMapper mapper)
        {
            _squipRepository = squipRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> ImportIdeas(IEnumerable<IdeaForCreationDto> ideasToImport)
        {
            var ideasToReturn = new Collection<IdeaDto>();
            foreach (var ideaForCreationDto in ideasToImport)
            {
                var ideaEntity = _mapper.Map<Idea>(ideaForCreationDto);

                await _squipRepository.Create(ideaEntity);

                var ideaToReturn = _mapper.Map<IdeaDto>(ideaEntity);
                ideasToReturn.Add(ideaToReturn);
            }

            return Ok(ideasToReturn);
        }
    }
}
