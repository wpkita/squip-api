using System;
using Microsoft.AspNetCore.Mvc;
using Squip.Rest.Dtos;

namespace Squip.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        public ActionResult<GameDto> Get()
        {
            return new GameDto(
                new IdeaDto(Guid.NewGuid(), "Hello"),
                new IdeaDto(Guid.NewGuid(), "World")
            );
        }
    }
}
