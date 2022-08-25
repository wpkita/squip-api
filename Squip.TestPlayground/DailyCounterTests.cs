using FluentAssertions;
using Xunit;

namespace Squip.TestPlayground;

public class DailyCounterTests
{
    [Fact]
    public void Activate_CurrentlyAt0_JumpsTo1()
    {
        var dailyCounter = new DailyCounter();

        dailyCounter.Activate();

        dailyCounter.Value.Should().Be(1);
    }

    [Fact]
    public void Activate_CurrentlyAt0_JumpsTo1_ThenJumpsTo2()
    {
        var dailyCounter = new DailyCounter();

        dailyCounter.Activate();
        dailyCounter.Value.Should().Be(1);

        dailyCounter.Activate();
        dailyCounter.Value.Should().Be(2);
    }
}
