using System;
using FluentAssertions;
using Xunit;

namespace Squip.TestPlayground
{
    public class EloScoreTests
    {
        [Fact]
        public void GetExpectedScore_SameScore_SameExpectedScore()
        {
            const double aRating = 400;
            const double bRating = 400;

            var aExpected = GetExpectedScore(aRating, bRating);
            aExpected.Should().Be(0.5);
            var bExpected = GetExpectedScore(bRating, aRating);
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
            GetNewRating(oldRating, expectedScore, actualScore).Should().Be(newRating);
        }

        private double GetNewRating(double oldRating, double expectedScore, double actualScore)
        {
            return oldRating + 32 * (actualScore - expectedScore);
        }

        private double GetExpectedScore(double myRating, double theirRating)
        {
            return 1 / (1 + Math.Pow(10, (theirRating - myRating) / 400));
        }
    }
}
