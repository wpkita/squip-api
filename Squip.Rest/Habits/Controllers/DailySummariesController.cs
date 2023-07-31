using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Squip.Rest.Habits.Dtos;
using Squip.Rest.Infrastructure.EntityFramework;
using Squip.Rest.Users.Services;

namespace Squip.Rest.Habits.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DailySummariesController : ControllerBase
{
    private readonly SquipContext _context;
    private readonly IUserIdProvider _userIdProvider;

    public DailySummariesController(SquipContext context, IUserIdProvider userIdProvider)
    {
        _context = context;
        _userIdProvider = userIdProvider;
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

        var percentile = 0.5;

        var dailyHabitGoal = await _context.DailyHabitSummaries
            .FromSqlInterpolated(
                $"select get_daily_habit_total_by_percentile({percentile}, {new Guid("55ce7706-7cac-47d0-90ca-1273d28bb1b6")}) as goal")
            .FirstAsync();
        var dailySummaryDto = new DailySummaryDto((int)Math.Ceiling(dailyHabitGoal.Goal), dailyTotalCount);

        return Ok(dailySummaryDto);
    }
}
