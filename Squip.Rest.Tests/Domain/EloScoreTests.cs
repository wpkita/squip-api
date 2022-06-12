using FluentAssertions;
using Squip.Rest.Domain;
using Xunit;

namespace Squip.Rest.Tests.Domain;

public class EloScoreTests
{
    [Fact]
    public void GetExpectedScore_SameScore_SameExpectedScore()
    {
        const double aRating = 400;
        const double bRating = 400;

        var aExpected = EloCalculator.GetExpectedScore(aRating, bRating);
        aExpected.Should().Be(0.5);
        var bExpected = EloCalculator.GetExpectedScore(bRating, aRating);
        bExpected.Should().Be(0.5);
    }

    [Theory]
    [InlineData(400, 0.5, 0, 384)]
    [InlineData(400, 0.5, 1, 416)]
    public void GetNewRating_Theory(
        double oldRating,
        double expectedScore,
        double actualScore,
        double newRating
    )
    {
        EloCalculator.GetNewRating(oldRating, expectedScore, actualScore).Should().Be(newRating);
    }
}
