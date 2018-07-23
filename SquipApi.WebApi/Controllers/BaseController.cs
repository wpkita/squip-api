using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SquipApi.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public abstract class BaseController : Controller
    {
    }
}
