using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckInsController : ControllerBase
{
    private readonly SquipContext _context;

    public CheckInsController(SquipContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CheckInForCreationDto checkInForCreationDto)
    {
        var moodEntries = checkInForCreationDto.MoodEntries.Select(IdeasProfile.MapDtoToMoodEntry).ToList();
        foreach (var moodEntry in moodEntries)
        {
            moodEntry.InstantOccurredAt = SystemClock.Instance.GetCurrentInstant();
        }
        _context.MoodEntries.AddRange(moodEntries);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
