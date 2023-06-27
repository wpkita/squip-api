using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squip.Rest.Habits.Domain;
using Squip.Rest.Habits.Dtos;
using Squip.Rest.Infrastructure.EntityFramework;

namespace Squip.Rest.Habits.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HabitsController : ControllerBase
{
    private readonly SquipContext _context;

    public HabitsController(SquipContext context)
    {
        _context = context;
    }

    // GET: api/Habits
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HabitDto>>> GetHabits()
    {
        var habitsFromDatabase = await _context.Habits.ToListAsync();

        var habitDtos = habitsFromDatabase.Select(HabitsProfile.MapHabitToDto);

        return Ok(habitDtos);
    }

    // GET: api/Habits/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHabit(Guid id)
    {
        var habit = await _context.Habits.FindAsync(id);

        if (habit == null) return NotFound();

        return Ok(HabitsProfile.MapHabitToDto(habit));
    }

    // PUT: api/Habits/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHabit(Guid id, HabitDto habitDto)
    {
        if (id != habitDto.Id) return BadRequest();

        var habit = await _context.Habits.FindAsync(id);

        if (habit == null) return NotFound();

        habit.Name = habitDto.Name;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HabitExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Habits
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Habit>> PostHabit(HabitForCreationDto habitForCreationDto)
    {
        var habit = HabitsProfile.MapDtoToHabit(habitForCreationDto);
        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();

        var habitDto = HabitsProfile.MapHabitToDto(habit);

        return CreatedAtAction("GetHabit", new { id = habitDto.Id }, habitDto);
    }

    // DELETE: api/Habits/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHabit(Guid id)
    {
        var habit = await _context.Habits.FindAsync(id);
        if (habit == null) return NotFound();

        _context.Habits.Remove(habit);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool HabitExists(Guid id)
    {
        return _context.Habits.Any(e => e.Id == id);
    }
}
