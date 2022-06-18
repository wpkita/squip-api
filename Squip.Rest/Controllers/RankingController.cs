using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squip.Rest.Domain;
using Squip.Rest.Dtos;
using Squip.Rest.Repositories;

namespace Squip.Rest.Controllers
{
    [ApiController]
    [Route("api/rankings")]
    public class RatingController : ControllerBase
    {
        private readonly SquipContext _context;

        public RatingController(SquipContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IEnumerable<IdeaRatingDto>> GetRatings()
        {
            var games = await _context.Games
                .Where(
                    game =>
                        game.Loser != null
                        && game.Winner != null
                        && !game.Loser.IsArchived
                        && !game.Winner.IsArchived
                )
                .OrderBy(game => game.InstantCreatedAt)
                .ToListAsync();

            var ratings = new Dictionary<Idea, double>(
                _context.Ideas.Select(idea => new KeyValuePair<Idea, double>(idea, 400))
            );

            foreach (var game in games)
            {
                var winnerExpectedScore = EloCalculator.GetExpectedScore(
                    ratings[game.Winner],
                    ratings[game.Loser]
                );
                var loserExpectedScore = EloCalculator.GetExpectedScore(
                    ratings[game.Loser],
                    ratings[game.Winner]
                );
                ratings[game.Winner] = EloCalculator.GetNewRating(
                    ratings[game.Winner],
                    winnerExpectedScore,
                    1
                );
                ratings[game.Loser] = EloCalculator.GetNewRating(
                    ratings[game.Loser],
                    loserExpectedScore,
                    0
                );
            }

            foreach (var idea in _context.Ideas)
            {
                idea.EloRating = ratings[idea];
            }

            await _context.SaveChangesAsync();

            var ratingDtos = ratings
                .Select(
                    rating => new IdeaRatingDto(IdeasProfile.MapIdeaToDto(rating.Key), rating.Value)
                )
                .OrderByDescending(rating => rating.Rating);

            return ratingDtos;
        }
    }

    public record IdeaRating(Idea Idea, double Rating);

    public record IdeaRatingDto(IdeaDto IdeaDto, double Rating);
}
