using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squip.Api.Repositories;
using Squip.Api.Dtos;
using Squip.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<PresentationDto> Inquire()
        {
            var user = _userService.GetCurrentUser();
            var presentation = await _squipService.Present(user);

            return presentation;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ValidationDto>> Ideate(IdeaDto ideaDto)
        {
            var user = _userService.GetCurrentUser();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validationDto = await _squipService.Ideate(user, ideaDto);

            return validationDto;
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<PresentationDto>> React(ReactionDto reactionDto)
        {
            var user = _userService.GetCurrentUser();
            if (reactionDto.PresentationId == null || reactionDto.ReactionCategory == null)
            {
                return BadRequest();
            }

            var presentation = await _squipService.React(user, reactionDto);

            return presentation;
        }
    }
}
