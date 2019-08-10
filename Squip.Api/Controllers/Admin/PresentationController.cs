using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squip.Data;
using Squip.Domain;

namespace Squip.Api.Controllers.Admin
{
    [Route("api/presentation")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]
    public class PresentationController : ControllerBase
    {
        private readonly IRepository<Presentation> _presentationRepository;

        public PresentationController(
            IRepository<Presentation> presentationRepository)
        {
            _presentationRepository = presentationRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Presentation>> GetById(string id)
        {
            var presentation = await _presentationRepository.GetById(id);

            if (presentation == null)
            {
                return NotFound();
            }

            return presentation;
        }

        [HttpGet]
        public async Task<IEnumerable<Presentation>> GetAll()
        {
            var presentations = await _presentationRepository.GetAll();

            return presentations;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Presentation presentation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _presentationRepository.Create(presentation);

            return CreatedAtAction(nameof(GetById), new { id = presentation.Id }, presentation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Presentation presentation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var doesPresentationExist = await _presentationRepository.DoesExistById(id);
            if (!doesPresentationExist)
            {
                return NotFound();
            }

            await _presentationRepository.Update(presentation);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var doesPresentationExist = await _presentationRepository.DoesExistById(id);
            if (!doesPresentationExist)
            {
                return NotFound();
            }

            await _presentationRepository.Archive(id);

            return NoContent();
        }
    }
}
