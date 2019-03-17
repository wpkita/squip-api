using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squip.Api.Repositories;
using Squip.Api.Dtos;
using Squip.Api.Services;

namespace Squip.Api.Controllers
{
    [Route("api/squip")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SquipController : ControllerBase
    {
        private readonly ISquipService _squipService;
        public SquipController(ISquipService squipService)
        {
            _squipService = squipService;
        }

        [HttpGet]
        public async Task<PresentationDto> InquirePresent()
        {
            var presentation = await _squipService.Present();

            return presentation;
        }

        [HttpPut]
        public async Task<ActionResult<PresentationDto>> ReactPresent(ReactionDto reactionDto)
        {
            if (reactionDto.PresentationId == null || reactionDto.ReactionCategory == null)
            {
                return BadRequest();
            }

            var presentation = await _squipService.ProcessReactionThenPresent(reactionDto);

            return presentation;
        }
    }
}
