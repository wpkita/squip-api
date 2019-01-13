using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squip.Api.Models;
using Squip.Api.Repositories;

namespace Squip.Api.Controllers
{
    [Route("api/squips")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SquipController : ControllerBase
    {
        private readonly ISquipRepository _squipRepository;
        public SquipController(ISquipRepository squipRepository)
        {
            _squipRepository = squipRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<SquipDto>> GetSquips()
        {
            return await _squipRepository.GetMostRecentSquipsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SquipDto>> GetSquip(long id)
        {
            var squip = await _squipRepository.GetSquipByIdAsync(id);

            if (squip == null)
            {
                return NotFound();
            }

            return squip;
        }

        [HttpPost]
        public async Task<ActionResult<SquipDto>> PostSquip(SquipDto squip)
        {
            await _squipRepository.CreateSquipAsync(squip);

            return CreatedAtAction("GetSquip", new { id = squip.Id }, squip);
        }

        [HttpPut]
        public async Task<IActionResult> PutSquip(long id, SquipDto squip)
        {
            if (id != squip.Id)
            {
                return BadRequest();
            }

            var squipFromDb = await _squipRepository.GetSquipByIdAsync(id);
            if (squipFromDb == null)
            {
                return NotFound();
            }

            await _squipRepository.UpdateSquipAsync(squip);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<SquipDto>> DeleteSquip(long id)
        {
            var squip = await _squipRepository.GetSquipByIdAsync(id);
            if (squip == null)
            {
                return NotFound();
            }

            await _squipRepository.DeleteSquipAsync(squip);
            return squip;
        }
    }
}
