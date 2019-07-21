using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squip.DomainModels;
using Squip.Api.Services;
using Squip.Repositories;

namespace Squip.Api.Controllers
{
    [Route("api/presentation")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]
    public class PresentationController : ControllerBase
    {
        private readonly IRepository<Presentation> presentationRepository;

        public PresentationController(
            IRepository<Presentation> presentationRepository)
        {
            this.presentationRepository = presentationRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Presentation>> GetById(string id)
        {
            var presentation = await presentationRepository.GetById(id);

            if (presentation == null)
            {
                return NotFound();
            }

            return presentation;
        }

        [HttpGet]
        public async Task<IEnumerable<Presentation>> GetAll()
        {
            var presentations = await presentationRepository.GetAll();

            return presentations;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Presentation presentation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await presentationRepository.Create(presentation);

            return CreatedAtAction(nameof(GetById), new { id = presentation.Id }, presentation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Presentation presentation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var doesPresentationExist = await presentationRepository.DoesExistById(id);
            if (!doesPresentationExist)
            {
                return NotFound();
            }

            await presentationRepository.Update(presentation);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var doesPresentationExist = await presentationRepository.DoesExistById(id);
            if (!doesPresentationExist)
            {
                return NotFound();
            }

            await presentationRepository.Archive(id);

            return NoContent();
        }
    }
}
