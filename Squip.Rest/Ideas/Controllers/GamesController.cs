using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squip.Rest.Ideas.Domain;
using Squip.Rest.Ideas.Dtos;
using Squip.Rest.Infrastructure.Common;
using Squip.Rest.Infrastructure.EntityFramework;

namespace Squip.Rest.Ideas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly SquipContext _context;
    private readonly ISquipRepository _squipRepository;

    public GamesController(ISquipRepository squipRepository, SquipContext context)
    {
        _squipRepository = squipRepository;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<GameDto>> GetAsync([FromQuery] string filter, CancellationToken cancellationToken)
    {
        var (leftIdea, rightIdea) = await _squipRepository.GetRandomIdeaPairAsync(
            filter,
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
        var game = await _context.FindAsync<Game>(
            new object[] { gameForUpdateDto.Id },
            cancellationToken
        );
        if (game == null)
            return NotFound();
        var winner = await _context.FindAsync<Idea>(
            new object[] { gameForUpdateDto.WinnerId },
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
