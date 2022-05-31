using System.Collections.Generic;
using FluentAssertions;
using Squip.Rest.Domain;
using Xunit;

namespace Squip.Rest.Tests.Domain
{
    public class HibitTests
    {
        [Fact]
        public void DiscreteFrameHasMaxScoreOf3_HighPerformanceDays_MaxesAt3()
        {
            var hibitWeek = new HibitWeek { ScoreGoal = 3 };

            var hibitMonday = new Hibit { Score = 1 };
            var hibitTuesday = new Hibit { Score = 1 };
            var hibitWednesday = new Hibit { Score = 1 };
            var hibitThursday = new Hibit { Score = 1 };
            var hibitFriday = new Hibit { Score = 1 };
            var hibitSaturday = new Hibit { Score = 1 };
            var hibitSunday = new Hibit { Score = 1 };

            hibitWeek.Hibits = new List<Hibit>
            {
                hibitMonday,
                hibitTuesday,
                hibitWednesday,
                hibitThursday,
                hibitFriday,
                hibitSaturday,
                hibitSunday
            };

            hibitWeek.RawScore.Should().Be(3);
            hibitWeek.NormalizedScore.Should().Be(1);
        }

        [Fact]
        public void DiscreteFrameHasMaxScoreOf3_LowPerformanceDays_IsLowScore()
        {
            var hibitWeek = new HibitWeek { ScoreGoal = 3 };

            var hibitMonday = new Hibit { Score = 1 };
            var hibitTuesday = new Hibit { Score = 0 };
            var hibitWednesday = new Hibit { Score = 0 };
            var hibitThursday = new Hibit { Score = 0 };
            var hibitFriday = new Hibit { Score = 0 };
            var hibitSaturday = new Hibit { Score = 0 };
            var hibitSunday = new Hibit { Score = 0 };

            hibitWeek.Hibits = new List<Hibit>
            {
                hibitMonday,
                hibitTuesday,
                hibitWednesday,
                hibitThursday,
                hibitFriday,
                hibitSaturday,
                hibitSunday
            };

            hibitWeek.RawScore.Should().Be(1);
            hibitWeek.NormalizedScore.Should().Be(1 / 3m);
        }

        [Fact]
        public void WeeklyScore_HasTwoHibitWeeksScaled_CalculatesWithScalingFactor()
        {
            var hibitWeek1 = new HibitWeek
            {
                ScoreGoal = 3,
                ScoreWeight = 0.25m,
                Hibits = new List<Hibit> { new(1), new(0), new(1) }
            };

            var hibitWeek2 = new HibitWeek
            {
                ScoreGoal = 50000,
                ScoreWeight = 0.75m,
                Hibits = new List<Hibit>
                {
                    new(6656),
                    new(4320),
                    new(5259),
                    new(7884),
                    new(2415),
                    new(2522),
                    new(2774)
                }
            };

            var week = new Week
            {
                HibitWeeks = new List<HibitWeek> { hibitWeek1, hibitWeek2 }
            };

            week.Score.Should().Be(1.93235m / 3);
        }
    }
}
