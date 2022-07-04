using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squip.Rest.Domain;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers
{
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
        public async Task<IActionResult> Post(LogDto logDto)
        {
            var log = new Log { Message = logDto.Message };
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
