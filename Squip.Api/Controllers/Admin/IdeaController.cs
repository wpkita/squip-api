using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squip.Data;
using Squip.Domain;
using Squip.Services;

namespace Squip.Api.Controllers.Admin
{
    [Route("api/idea")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]

    public class IdeaController : ControllerBase
    {
        private readonly IRepository<Idea> _ideaRepository;
        private readonly ISquipService _squipService;

        public IdeaController(
            IRepository<Idea> ideaRepository,
            ISquipService squipService)
        {
            _ideaRepository = ideaRepository;
            _squipService = squipService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Idea>> GetById(string id)
        {
            var idea = await _ideaRepository.GetById(id);

            if (idea == null)
            {
                return NotFound();
            }

            return idea;
        }

        [HttpGet]
        public async Task<IEnumerable<Idea>> GetAll()
        {
            var ideas = await _ideaRepository.GetAll();

            return ideas;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Idea idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _squipService.Ideate(idea);

            return CreatedAtAction(nameof(GetById), new { id = idea.Id }, idea);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Idea idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var doesIdeaExist = await _ideaRepository.DoesExistById(id);
            if (!doesIdeaExist)
            {
                return NotFound();
            }

            await _ideaRepository.Update(idea);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var doesIdeaExist = await _ideaRepository.DoesExistById(id);
            if (!doesIdeaExist)
            {
                return NotFound();
            }

            await _ideaRepository.Archive(id);

            return NoContent();
        }
    }
}
