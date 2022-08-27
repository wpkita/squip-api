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

[Route("api/habits/{habitId}/[controller]")]
[ApiController]
public class HibitsController : ControllerBase
{
    private readonly SquipContext _context;

    public HibitsController(SquipContext context)
    {
        _context = context;
    }

    // GET: api/Hibits
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HibitDto>>> GetHibits()
    {
        var hibitsFromDatabase = await _context.Hibits.ToListAsync();

        var hibitDtos = hibitsFromDatabase.Select(IdeasProfile.MapHibitToDto);

        return Ok(hibitDtos);
    }

    // GET: api/Hibits/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHibit(Guid id)
    {
        var hibit = await _context.Hibits.FindAsync(id);

        if (hibit == null) return NotFound();

        return Ok(IdeasProfile.MapHibitToDto(hibit));
    }

    // POST: api/Hibits
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Hibit>> PostHibit(Guid habitId, HibitForCreationDto hibitForCreationDto)
    {
        if (habitId != hibitForCreationDto.HabitId) return BadRequest();

        var habit = await _context.Habits.FindAsync(habitId);
        if (habit == null) return NotFound();

        var hibit = IdeasProfile.MapDtoToHibit(hibitForCreationDto);
        hibit.InstantOccurredAt = SystemClock.Instance.GetCurrentInstant();
        _context.Hibits.Add(hibit);
        await _context.SaveChangesAsync();

        var hibitDto = IdeasProfile.MapHibitToDto(hibit);

        return CreatedAtAction("GetHibit", new { habitId = hibitDto.HabitId, id = hibitDto.Id }, hibitDto);
    }

    // DELETE: api/Hibits/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHibit(Guid habitId, Guid id)
    {
        var habit = await _context.Habits.FindAsync(habitId);
        if (habit == null) return NotFound();

        var hibit = await _context.Hibits.FindAsync(id);
        if (hibit == null) return NotFound();

        _context.Hibits.Remove(hibit);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
