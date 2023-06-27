using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Squip.Rest.Habits.Dtos;
using Squip.Rest.Infrastructure.EntityFramework;

namespace Squip.Rest.Habits.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HabitSummariesController : ControllerBase
{
    private readonly SquipContext _context;

    public HabitSummariesController(SquipContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HabitSummaryDto>>> GetAsync(string timeZone)
    {
        var zone = DateTimeZoneProviders.Tzdb[timeZone];
        var instant = SystemClock.Instance.GetCurrentInstant();
        var zonedDateTime = new ZonedDateTime(instant, zone);
        var date = zonedDateTime.Date;
        var startOfDate = date.AtStartOfDayInZone(zone).ToInstant();
        var endOfDate = startOfDate.Plus(Duration.FromDays(1));
        var habitSummaries = await _context.Habits
            .Select(habit => new HabitSummaryDto(new HabitDto(habit.Id, habit.Name),
                habit.Hibits.Count(hibit => hibit.InstantOccurredAt >= startOfDate && hibit.InstantOccurredAt < endOfDate))).ToListAsync();

        return Ok(habitSummaries);
    }
}
