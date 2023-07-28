using System;
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
public class DailySummariesController : ControllerBase
{
    private readonly SquipContext _context;

    public DailySummariesController(SquipContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DailySummaryDto>>> GetAsync(string timeZone)
    {
        var zone = DateTimeZoneProviders.Tzdb[timeZone];
        var instant = SystemClock.Instance.GetCurrentInstant();
        var zonedDateTime = new ZonedDateTime(instant, zone);
        var date = zonedDateTime.Date;
        var startOfDate = date.AtStartOfDayInZone(zone).ToInstant();
        var endOfDate = startOfDate.Plus(Duration.FromDays(1));
        var dailyTotalCount = await _context.Hibits.CountAsync(
            hibit => hibit.InstantOccurredAt >= startOfDate && hibit.InstantOccurredAt < endOfDate
        );

        var targetTotalCount = 0;

        var dailySummaryDto = new DailySummaryDto(targetTotalCount, dailyTotalCount);

        return Ok(dailySummaryDto);
    }
}
