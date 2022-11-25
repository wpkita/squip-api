using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squip.Rest.Domain;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DailySummaryController : ControllerBase
{
    private readonly SquipContext _context;

    public DailySummaryController(SquipContext context)
    {
        _context = context;
    }

    // GET: api/DailySummary
    [HttpGet]
    public async Task<ActionResult<DailySummaryDto>> GetDailySummary()
    {
        var habitSummaries = await _context.Habits
            .Select(habit => new HabitSummaryDto(habit.Id, habit.Name, habit.Hibits.Count)).ToListAsync();

        var dailySummary = new DailySummaryDto(habitSummaries);

        return Ok(dailySummary);
    }
}
