using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squip.Data;
using Squip.Domain;
using Squip.RestApi.Dtos;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Squip.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkIdeasController : ControllerBase
    {
        private readonly IRepository<Idea> _squipRepository;
        private readonly IMapper _mapper;

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
                var ideaEntity =_mapper.Map<Idea>(ideaForCreationDto);

                await _squipRepository.Create(ideaEntity);

                var ideaToReturn = _mapper.Map<IdeaDto>(ideaEntity);
                ideasToReturn.Add(ideaToReturn);
            }

            return Ok(ideasToReturn);
        }
    }
}
