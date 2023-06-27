using System;

namespace Squip.Rest.Ideas.Domain;

public static class EloCalculator
{
    private const int K = 32;

    public static double GetNewRating(double oldRating, double expectedScore, double actualScore)
    {
        return oldRating + K * (actualScore - expectedScore);
    }

    public static double GetExpectedScore(double myRating, double theirRating)
    {
        return 1 / (1 + Math.Pow(10, (theirRating - myRating) / 400));
    }
}
