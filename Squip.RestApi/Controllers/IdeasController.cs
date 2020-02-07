using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squip.Data;
using Squip.Domain;
using Squip.RestApi.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squip.RestApi.Controllers
{
    [ApiController]
    [Route("api/ideas")]
    public class IdeasController : ControllerBase
    {
        private readonly IRepository<Idea> _ideasRepository;
        private readonly IMapper _mapper;

        public IdeasController(IRepository<Idea> ideasRepository, IMapper mapper)
        {
            _ideasRepository = ideasRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IEnumerable<IdeaDto>> GetIdeas()
        {
            var ideasFromRepo = await _ideasRepository.GetAll();

            return _mapper.Map<IEnumerable<IdeaDto>>(ideasFromRepo);
        }

        [HttpGet("{ideaId}", Name = "GetIdea")]
        public async Task<IActionResult> GetIdea(Guid ideaId)
        {
            var ideaFromRepo = await _ideasRepository.GetById(ideaId.ToString());

            if (ideaFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IdeaDto>(ideaFromRepo));
        }

        [HttpPost]
        public async Task<ActionResult<IdeaDto>> CreateIdea(IdeaForCreationDto idea)
        {
            var ideaEntity = _mapper.Map<Idea>(idea);
            await _ideasRepository.Create(ideaEntity);

            var ideaToReturn = _mapper.Map<IdeaDto>(ideaEntity);
            return CreatedAtRoute("GetIdea", new {ideaId = ideaToReturn.Id}, ideaToReturn);
        }
    }
}
