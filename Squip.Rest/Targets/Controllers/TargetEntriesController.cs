using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Squip.Rest.Infrastructure.EntityFramework;
using Squip.Rest.Targets.Domain;
using Squip.Rest.Targets.Dtos;

namespace Squip.Rest.Targets.Controllers;

[Route("api/targets/{targetId}/[controller]")]
[ApiController]
public class TargetEntriesController : ControllerBase
{
    private readonly SquipContext _context;

    public TargetEntriesController(SquipContext context)
    {
        _context = context;
    }

    // GET: api/TargetEntries/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTargetEntry(Guid id)
    {
        var targetEntry = await _context.TargetEntries.FindAsync(id);

        if (targetEntry == null) return NotFound();

        return Ok(TargetsProfile.MapTargetEntryToDto(targetEntry));
    }

    // POST: api/TargetEntries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TargetEntry>> PostTargetEntry(Guid targetId, TargetEntryForCreationDto targetEntryForCreationDto)
    {
        if (targetId != targetEntryForCreationDto.TargetId) return BadRequest();

        var target = await _context.Targets.FindAsync(targetId);
        if (target == null) return NotFound();

        var targetEntry = TargetsProfile.MapDtoToTargetEntry(targetEntryForCreationDto);
        targetEntry.InstantOccurredAt = SystemClock.Instance.GetCurrentInstant();
        _context.TargetEntries.Add(targetEntry);
        await _context.SaveChangesAsync();

        var targetEntryDto = TargetsProfile.MapTargetEntryToDto(targetEntry);

        return CreatedAtAction("GetTargetEntry", new { targetId = targetEntryDto.TargetId, id = targetEntryDto.Id }, targetEntryDto);
    }
}
