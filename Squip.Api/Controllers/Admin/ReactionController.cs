using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squip.DomainModels;
using Squip.Api.Services;
using Squip.Repositories;

namespace Squip.Api.Controllers
{
    [Route("api/reaction")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]

    public class ReactionController : ControllerBase
    {
        private readonly IRepository<Reaction> reactionRepository;
        private readonly ISquipService squipService;

        public ReactionController(
            IRepository<Reaction> reactionRepository,
            ISquipService squipService)
        {
            this.reactionRepository = reactionRepository;
            this.squipService = squipService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reaction>> GetById(string id)
        {
            var reaction = await reactionRepository.GetById(id);

            if (reaction == null)
            {
                return NotFound();
            }

            return reaction;
        }

        [HttpGet]
        public async Task<IEnumerable<Reaction>> GetAll()
        {
            var reactions = await reactionRepository.GetAll();

            return reactions;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Reaction reaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await squipService.React(reaction);

            return CreatedAtAction(nameof(GetById), new { id = reaction.Id }, reaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Reaction reaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var doesReactionExist = await reactionRepository.DoesExistById(id);
            if (!doesReactionExist)
            {
                return NotFound();
            }

            await reactionRepository.Update(reaction);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var doesReactionExist = await reactionRepository.DoesExistById(id);
            if (!doesReactionExist)
            {
                return NotFound();
            }

            await reactionRepository.Archive(id);

            return NoContent();
        }
    }
}
