using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squip.Api.DomainModels;
using Squip.Api.Repositories;
using Squip.Api.Services;

namespace Squip.Api.Controllers
{
    [Route("api/idea")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    // [Authorize]

    public class IdeaController : ControllerBase
    {
        private readonly IRepository<Idea> ideaRepository;
        private readonly ISquipService squipService;

        public IdeaController(
            IRepository<Idea> ideaRepository,
            ISquipService squipService)
        {
            this.ideaRepository = ideaRepository;
            this.squipService = squipService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Idea>> GetById(string id)
        {
            var idea = await ideaRepository.GetById(id);

            if (idea == null)
            {
                return NotFound();
            }

            return idea;
        }

        [HttpGet]
        public async Task<IEnumerable<Idea>> GetAll()
        {
            var ideas = await ideaRepository.GetAll();

            return ideas;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Idea idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await squipService.Ideate(idea);

            return CreatedAtAction(nameof(GetById), new { id = idea.Id }, idea);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Idea idea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var doesIdeaExist = await ideaRepository.DoesExistById(id);
            if (!doesIdeaExist)
            {
                return NotFound();
            }

            await ideaRepository.Update(idea);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var doesIdeaExist = await ideaRepository.DoesExistById(id);
            if (!doesIdeaExist)
            {
                return NotFound();
            }

            await ideaRepository.Archive(id);

            return NoContent();
        }
    }
}
