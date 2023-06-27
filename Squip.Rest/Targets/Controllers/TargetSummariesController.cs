using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Squip.Rest.Infrastructure.EntityFramework;
using Squip.Rest.Targets.Dtos;

namespace Squip.Rest.Targets.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TargetSummariesController : ControllerBase
{
    private readonly SquipContext _context;

    public TargetSummariesController(SquipContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TargetSummaryDto>>> GetAsync(string timeZone)
    {
        var zone = DateTimeZoneProviders.Tzdb[timeZone];
        var instant = SystemClock.Instance.GetCurrentInstant();
        var zonedDateTime = new ZonedDateTime(instant, zone);
        var date = zonedDateTime.Date;
        var startOfDate = date.AtStartOfDayInZone(zone).ToInstant();
        var endOfDate = startOfDate.Plus(Duration.FromDays(1));
        var targetSummaries = await _context.Targets
            .Select(target => new TargetSummaryDto(new TargetDto(target.Id, target.Name),
                target.TargetEntries.Count(targetEntry => targetEntry.InstantOccurredAt >= startOfDate && targetEntry.InstantOccurredAt < endOfDate))).ToListAsync();

        return Ok(targetSummaries);
    }
}
