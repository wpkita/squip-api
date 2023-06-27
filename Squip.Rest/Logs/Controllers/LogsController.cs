using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squip.Rest.Infrastructure.EntityFramework;
using Squip.Rest.Logs.Domain;
using Squip.Rest.Logs.Dtos;

namespace Squip.Rest.Logs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly SquipContext _context;

    public LogsController(SquipContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        LogDto logDto,
        CancellationToken cancellationToken
    )
    {
        var log = new Log { Message = logDto.Message };
        _context.Logs.Add(log);
        await _context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
