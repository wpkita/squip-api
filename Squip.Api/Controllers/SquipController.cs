using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squip.Api.Models;

namespace Squip.Api.Controllers
{
    [Route("api/squips")]
    [ApiController]
    public class SquipController : ControllerBase
    {
        private readonly SquipContext _context;

        public SquipController(SquipContext context)
        {
            _context = context;

            if (_context.Squips.Count() == 0)
            {
                _context.Squips.Add(new SquipDto { Title = "title 1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SquipDto>>> GetSquips()
        {
            return await _context.Squips.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SquipDto>> GetSquip(long id)
        {
            var squip = await _context.Squips.FindAsync(id);

            if (squip == null)
            {
                return NotFound();
            }

            return squip;
        }

        [HttpPost]
        public async Task<ActionResult<SquipDto>> PostSquip(SquipDto squip)
        {
            _context.Squips.Add(squip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSquip", new { id = squip.Id }, squip);
        }

        [HttpPut]
        public async Task<IActionResult> PutSquip(long id, SquipDto squip)
        {
            if (id != squip.Id)
            {
                return BadRequest();
            }

            _context.Entry(squip).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<SquipDto>> DeleteSquip(long id)
        {
            var squip = await _context.Squips.FindAsync(id);
            if (squip == null)
            {
                return NotFound();
            }

            _context.Squips.Remove(squip);
            await _context.SaveChangesAsync();

            return squip;
        }
    }
}
