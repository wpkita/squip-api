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

        const double percentile = 0.9;

        var oidcSub = _userIdProvider.GetCurrentUserId();
        var user = await _context.Users.SingleAsync(u => u.OidcSub == oidcSub);

        var dailyHabitGoal = await _context.DailyHabitSummaries
            .FromSqlInterpolated(
                $"select habit_total_by_percentile({percentile}, {timeZone}, {user.Id}) as goal")
            .FirstAsync();
        var dailySummaryDto = new DailySummaryDto((int)Math.Ceiling(dailyHabitGoal.Goal), dailyTotalCount);

        return Ok(dailySummaryDto);
    }
}
