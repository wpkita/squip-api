using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TargetsController : ControllerBase
{
    private readonly SquipContext _context;

    public TargetsController(SquipContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var targets = await _context.Targets.ToListAsync();

        var targetDtos = targets.Select(IdeasProfile.MapTargetToDto);

        return Ok(targetDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var target = await _context.Targets.FindAsync(id);

        if (target == null) return NotFound();

        return Ok(IdeasProfile.MapTargetToDto(target));
    }

    [HttpPost]
    public async Task<ActionResult<TargetDto>> PostAsync(TargetForCreationDto targetForCreationDto)
    {
        var target = IdeasProfile.MapDtoToTarget(targetForCreationDto);
        _context.Targets.Add(target);
        await _context.SaveChangesAsync();

        var targetDto = IdeasProfile.MapTargetToDto(target);

        return CreatedAtAction("Get", new { id = targetDto.Id }, targetDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, TargetDto targetDto)
    {
        if (id != targetDto.Id) return BadRequest();

        var target = await _context.Targets.FindAsync(id);

        if (target == null) return NotFound();

        target.Name = targetDto.Name;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TargetExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    private bool TargetExists(Guid id)
    {
        return _context.Targets.Any(m => m.Id == id);
    }
}
