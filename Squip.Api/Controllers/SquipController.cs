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
    [Route("api/squips")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SquipController : ControllerBase
    {
        private readonly ISquipRepository _squipRepository;
        private readonly IMapper _mapper;
        public SquipController(IMapper mapper, ISquipRepository squipRepository)
        {
            _mapper = mapper;
            _squipRepository = squipRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<SquipDto>> GetSquips()
        {
            var squipPocos = await _squipRepository.GetMostRecentSquipsAsync();
            var squipDtos = _mapper.Map<IEnumerable<SquipPoco>, IEnumerable<SquipDto>>(squipPocos);

            return squipDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SquipDto>> GetSquip(long id)
        {
            var squipPoco = await _squipRepository.GetSquipByIdAsync(id);

            if (squipPoco == null)
            {
                return NotFound();
            }

            var squipDto = _mapper.Map<SquipPoco, SquipDto>(squipPoco);

            return squipDto;
        }

        [HttpPost]
        public async Task<ActionResult<SquipDto>> PostSquip(SquipDto squipDto)
        {
            var squipPoco = _mapper.Map<SquipDto, SquipPoco>(squipDto);

            await _squipRepository.CreateSquipAsync(squipPoco);

            _mapper.Map<SquipPoco, SquipDto>(squipPoco, squipDto);

            return CreatedAtAction("GetSquip", new { id = squipDto.Id }, squipDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSquip(long id, SquipDto squipDto)
        {
            if (id != squipDto.Id)
            {
                return BadRequest();
            }

            var squipPoco = await _squipRepository.GetSquipByIdAsync(id);
            if (squipPoco == null)
            {
                return NotFound();
            }

            _mapper.Map<SquipDto, SquipPoco>(squipDto, squipPoco);

            await _squipRepository.UpdateSquipAsync(squipPoco);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SquipDto>> DeleteSquip(long id)
        {
            var squipPoco = await _squipRepository.GetSquipByIdAsync(id);
            if (squipPoco == null)
            {
                return NotFound();
            }

            await _squipRepository.DeleteSquipAsync(squipPoco);

            var squipDto = _mapper.Map<SquipPoco, SquipDto>(squipPoco);
            return squipDto;
        }
    }
}
