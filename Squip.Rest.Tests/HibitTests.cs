using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Squip.Rest.Tests
{
    public class HibitTests
    {
        [Fact]
        public void DiscreteFrameHasMaxScoreOf3_HighPerformanceDays_MaxesAt3()
        {
            var week = new Week
            {
                ScoreGoal = 3
            };

            var hibitMonday = new Hibit
            {
                Score = 1
            };
            var hibitTuesday = new Hibit
            {
                Score = 1
            };
            var hibitWednesday = new Hibit
            {
                Score = 1
            };
            var hibitThursday = new Hibit
            {
                Score = 1
            };
            var hibitFriday = new Hibit
            {
                Score = 1
            };
            var hibitSaturday = new Hibit
            {
                Score = 1
            };
            var hibitSunday = new Hibit
            {
                Score = 1
            };

            week.Hibits = new List<Hibit>
            {
                hibitMonday,
                hibitTuesday,
                hibitWednesday,
                hibitThursday,
                hibitFriday,
                hibitSaturday,
                hibitSunday
            };

            week.RawScore.Should().Be(3);
            week.NormalizedScore.Should().Be(1);
        }

        [Fact]
        public void DiscreteFrameHasMaxScoreOf3_LowPerformanceDays_IsLowScore()
        {
            var week = new Week
            {
                ScoreGoal = 3
            };

            var hibitMonday = new Hibit
            {
                Score = 1
            };
            var hibitTuesday = new Hibit
            {
                Score = 0
            };
            var hibitWednesday = new Hibit
            {
                Score = 0
            };
            var hibitThursday = new Hibit
            {
                Score = 0
            };
            var hibitFriday = new Hibit
            {
                Score = 0
            };
            var hibitSaturday = new Hibit
            {
                Score = 0
            };
            var hibitSunday = new Hibit
            {
                Score = 0
            };

            week.Hibits = new List<Hibit>
            {
                hibitMonday,
                hibitTuesday,
                hibitWednesday,
                hibitThursday,
                hibitFriday,
                hibitSaturday,
                hibitSunday
            };

            week.RawScore.Should().Be(1);
            week.NormalizedScore.Should().Be(1 / 3m);
        }

        [Fact]
        public void TestScalingFactor()
        {
        }
    }

    public class Hibit
    {
        public decimal Score { get; set; }
    }

    public class Week
    {
        public IList<Hibit> Hibits { get; set; }
        public decimal ScoreGoal { get; set; }

        public decimal RawScore
        {
            get { return Math.Min(ScoreGoal, Hibits.Sum(h => h.Score)); }
        }

        public decimal NormalizedScore => RawScore / ScoreGoal;
    }
}
