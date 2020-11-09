using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Squip.Rest.Controllers
{
    [Route("0.1/[controller]")]
    [ApiController]
    public class TilesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"Hello", "World"};
        }
    }
}
