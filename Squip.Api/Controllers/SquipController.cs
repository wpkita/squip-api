using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squip.Api.Repositories;
using Squip.Api.Dtos;
using Squip.Api.Services;
using Microsoft.AspNetCore.Http;

namespace Squip.Api.Controllers
{
    [Route("api/squip")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SquipController : ControllerBase
    {
        private readonly ISquipService _squipService;
        private readonly IUserService _userService;

        public SquipController(ISquipService squipService, IUserService userService)
        {
            _userService = userService;
            _squipService = squipService;
        }

        [HttpGet]
        public async Task<PresentationDto> InquirePresent()
        {
            var user = _userService.GetCurrentUser();
            var presentation = await _squipService.Present(user);

            return presentation;
        }

        [HttpPut]
        public async Task<ActionResult<PresentationDto>> ReactPresent(ReactionDto reactionDto)
        {
            var user = _userService.GetCurrentUser();
            if (reactionDto.PresentationId == null || reactionDto.ReactionCategory == null)
            {
                return BadRequest();
            }

            var presentation = await _squipService.ProcessReactionThenPresent(user, reactionDto);

            return presentation;
        }
    }
}
