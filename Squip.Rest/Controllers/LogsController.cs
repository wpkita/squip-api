using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squip.Rest.Domain;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers;

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
