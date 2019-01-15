using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squip.Api.Models;
using Squip.EntityFramework.Repositories;
using Squip.Pocos;

namespace Squip.Api.Controllers
{
    [Route("api/squips/{squipId}/tags")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SquipTagController : ControllerBase
    {
        private readonly ISquipRepository _squipRepository;
        private readonly IMapper _mapper;
        public SquipTagController(IMapper mapper, ISquipRepository squipRepository)
        {
            _mapper = mapper;
            _squipRepository = squipRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetSquipTags(long squipId)
        {
            var squipPoco = await _squipRepository.GetSquipByIdAsync(squipId);
            if (squipPoco == null)
            {
                return NotFound();
            }

            var tagPocos = await _squipRepository.GetTagsBySquipId(squipId);

            var tagDtos = _mapper.Map<IEnumerable<TagPoco>, IEnumerable<TagDto>>(tagPocos).ToList();

            return tagDtos;
        }

        [HttpPost]
        public async Task<IActionResult> AddTagToSquip(long squipId, string tag)
        {
            var squipPoco = await _squipRepository.GetSquipByIdAsync(squipId);
            if (squipPoco == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(tag))
            {
                return BadRequest();
            }

            await _squipRepository.AddTagToSquip(squipPoco, tag);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveTagFromSquip(long squipId, string tag)
        {
            var squipPoco = await _squipRepository.GetSquipByIdAsync(squipId);
            if (squipPoco == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(tag))
            {
                return BadRequest();
            }

            await _squipRepository.RemoveTagFromSquip(squipPoco, tag);

            return NoContent();
        }
    }
}
