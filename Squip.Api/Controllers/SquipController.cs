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
using Squip.Api.DomainModels;
using AutoMapper;

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
            // Map to domain model
            var user = _userService.GetCurrentUser();
            var inquiry = new Inquiry { UserId = user.Id };

            // Call service
            var presentation = await _squipService.Inquire(inquiry);

            // Map to dto
            var presentationDto = Mapper.Map<PresentationDto>(presentation);

            // Return dto
            return presentationDto;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Ideate(IdeaDto ideaDto)
        {
            // Validate dto
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Map to domain model
            var idea = Mapper.Map<Idea>(ideaDto);
            var user = _userService.GetCurrentUser();
            idea.UserId = user.Id;

            // Call service
            await _squipService.Ideate(idea);

            // Return dto
            return Ok();
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<PresentationDto>> React(ReactionDto reactionDto)
        {
            // Validate dto
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Map to domain model
            var reaction = Mapper.Map<Reaction>(reactionDto);
            var user = _userService.GetCurrentUser();
            reaction.UserId = user.Id;

            // Call service
            var presentation = await _squipService.React(reaction);

            // Map to dto
            var presentationDto = Mapper.Map<PresentationDto>(presentation);

            // Return dto
            return presentationDto;
        }
    }
}
