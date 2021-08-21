using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Squip.Rest.Tests
{
    public class Tests
    {
        [Fact]
        public void GoalHasMaxScoreOfFive_HighPerformanceDays_MaxesAt5()
        {
            GoalWeek goalWeek = new GoalWeek
            {
                MaxScore = 5
            };
            
            Hibit hibitMonday = new Hibit
            {
                Score = 1
            };
            Hibit hibitTuesday = new Hibit
            {
                Score = 1
            };
            Hibit hibitWednesday = new Hibit
            {
                Score = 1
            };
            Hibit hibitThursday = new Hibit
            {
                Score = 1
            };
            Hibit hibitFriday = new Hibit
            {
                Score = 1
            };
            Hibit hibitSaturday = new Hibit
            {
                Score = 1
            };
            Hibit hibitSunday = new Hibit
            {
                Score = 1
            };

            goalWeek.Hibits = new List<Hibit>
            {
                hibitMonday,
                hibitTuesday,
                hibitWednesday,
                hibitThursday,
                hibitFriday,
                hibitSaturday,
                hibitSunday
            };

            goalWeek.Score.Should().Be(5);
        }
    }

    public class Hibit
    {
        public int Score { get; set; }
    }

    public class GoalWeek
    {
        public IList<Hibit> Hibits { get; set; }
        public int MaxScore { get; set; }

        public decimal Score
        {
            get
            {
                return Math.Min(MaxScore, Hibits.Sum(h => h.Score));
            }
        }
    }
}
