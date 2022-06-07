using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squip.Rest.Domain;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ISquipRepository _squipRepository;
        private readonly IMapper _mapper;
        private readonly SquipContext _context;

        public GamesController(
            ISquipRepository squipRepository,
            IMapper mapper,
            SquipContext context
        )
        {
            _squipRepository = squipRepository;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<GameDto>> Get()
        {
            var (leftIdea, rightIdea) = await _squipRepository.GetRandomIdeaPair();

            var game = new Game { Left = leftIdea, Right = rightIdea };
            game.PreCreate();
            await _context.AddAsync(game);
            await _context.SaveChangesAsync();

            var gameDto = _mapper.Map<Game, GameDto>(game);

            return gameDto;
        }

        [HttpPut]
        public async Task<ActionResult<GameDto>> Put([FromBody] GameForUpdateDto gameForUpdateDto)
        {
            var game = await _context.FindAsync<Game>(gameForUpdateDto.Id);
            if (game == null)
                return NotFound();
            var winner = await _context.FindAsync<Idea>(gameForUpdateDto.WinnerId);
            if (winner == null)
                return BadRequest();

            game.SetWinner(winner);
            game.PreUpdate();
            await _context.SaveChangesAsync();

            var gameDto = _mapper.Map<Game, GameDto>(game);

            return Ok(gameDto);
        }
    }
}
