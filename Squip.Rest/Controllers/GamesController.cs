using System;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly SquipContext _context;

        public GamesController(ISquipRepository squipRepository, SquipContext context)
        {
            _squipRepository = squipRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<GameDto>> GetAsync(CancellationToken cancellationToken)
        {
            var (leftIdea, rightIdea) = await _squipRepository.GetRandomIdeaPairAsync(
                cancellationToken
            );

            var game = new Game { Left = leftIdea, Right = rightIdea };
            await _context.AddAsync(game, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var gameDto = IdeasProfile.MapGameToDto(game);

            return gameDto;
        }

        [HttpPut]
        public async Task<ActionResult<GameDto>> PutAsync(
            [FromBody] GameForUpdateDto gameForUpdateDto,
            CancellationToken cancellationToken
        )
        {
            var game = await _context.FindAsync<Game>(gameForUpdateDto.Id, cancellationToken);
            if (game == null)
                return NotFound();
            var winner = await _context.FindAsync<Idea>(
                gameForUpdateDto.WinnerId,
                cancellationToken
            );
            if (winner == null)
                return BadRequest();

            game.SetWinner(winner);
            await _context.SaveChangesAsync(cancellationToken);

            var gameDto = IdeasProfile.MapGameToDto(game);

            return Ok(gameDto);
        }
    }
}
