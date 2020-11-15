using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers
{
    [Route("api/squips")]
    [ApiController]
    public class SquipsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISquipRepository _squipRepository;

        public SquipsController(ISquipRepository squipRepository, IMapper mapper)
        {
            _squipRepository = squipRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSquip()
        {
            var squipFromRepo = await _squipRepository.GetRandomIdea();

            if (squipFromRepo == null) return NotFound();

            return Ok(_mapper.Map<IdeaDto>(squipFromRepo));
        }
    }
}
