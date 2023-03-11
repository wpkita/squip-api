using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Squip.Rest.Domain;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers;

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

        return Ok(IdeasProfile.MapTargetEntryToDto(targetEntry));
    }

    // POST: api/TargetEntries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TargetEntry>> PostTargetEntry(Guid targetId, TargetEntryForCreationDto targetEntryForCreationDto)
    {
        if (targetId != targetEntryForCreationDto.TargetId) return BadRequest();

        var target = await _context.Targets.FindAsync(targetId);
        if (target == null) return NotFound();

        var targetEntry = IdeasProfile.MapDtoToTargetEntry(targetEntryForCreationDto);
        targetEntry.InstantOccurredAt = SystemClock.Instance.GetCurrentInstant();
        _context.TargetEntries.Add(targetEntry);
        await _context.SaveChangesAsync();

        var targetEntryDto = IdeasProfile.MapTargetEntryToDto(targetEntry);

        return CreatedAtAction("GetTargetEntry", new { targetId = targetEntryDto.TargetId, id = targetEntryDto.Id }, targetEntryDto);
    }
}
