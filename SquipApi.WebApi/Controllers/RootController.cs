using Microsoft.AspNetCore.Mvc;

namespace SquipApi.WebApi.Controllers
{
    [Route("")]
    [ApiController]
    public class RootController : Controller
    {
        [HttpGet]
        public IActionResult Root()
        {
            return NoContent();
        }
    }
}
