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
public class MoodsController : ControllerBase
{
    private readonly SquipContext _context;

    public MoodsController(SquipContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var moods = await _context.Moods.ToListAsync();

        var moodDtos = moods.Select(IdeasProfile.MapMoodToDto);

        return Ok(moodDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var mood = await _context.Moods.FindAsync(id);

        if (mood == null) return NotFound();

        return Ok(IdeasProfile.MapMoodToDto(mood));
    }

    [HttpPost]
    public async Task<ActionResult<MoodDto>> PostAsync(MoodForCreationDto moodForCreationDto)
    {
        var mood = IdeasProfile.MapDtoToMood(moodForCreationDto);
        _context.Moods.Add(mood);
        await _context.SaveChangesAsync();

        var moodDto = IdeasProfile.MapMoodToDto(mood);

        return CreatedAtAction("Get", new { id = moodDto.Id }, moodDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, MoodDto moodDto)
    {
        if (id != moodDto.Id) return BadRequest();

        var mood = await _context.Moods.FindAsync(id);

        if (mood == null) return NotFound();

        mood.Name = moodDto.Name;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MoodExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    private bool MoodExists(Guid id)
    {
        return _context.Moods.Any(m => m.Id == id);
    }
}
