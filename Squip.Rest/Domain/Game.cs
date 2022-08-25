using System;

namespace Squip.Rest.Domain;

public class Game : DomainModelBase
{
    public Idea Left { get; set; }
    public Idea Right { get; set; }
    public Idea Winner { get; set; }
    public Idea Loser { get; set; }

    public void SetWinner(Idea winner)
    {
        if (Left != winner && Right != winner)
            throw new ArgumentException("Winner must be a participant in the game.");

        Winner = winner;
        Loser = Left == winner ? Right : Left;

        UpdateRatings();
    }

    private void UpdateRatings()
    {
        var winnerExpectedScore = EloCalculator.GetExpectedScore(
            Winner.EloRating,
            Loser.EloRating
        );
        var loserExpectedScore = EloCalculator.GetExpectedScore(
            Loser.EloRating,
            Winner.EloRating
        );

        Winner.EloRating = EloCalculator.GetNewRating(Winner.EloRating, winnerExpectedScore, 1);
        Loser.EloRating = EloCalculator.GetNewRating(Loser.EloRating, loserExpectedScore, 0);
    }
}
