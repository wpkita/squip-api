using FluentAssertions;
using Xunit;

namespace Squip.TestPlayground;

public class DailyToggleTests
{
    [Fact]
    public void Toggle_WasFalse_NowIsTrue()
    {
        var dailyToggle = new DailyToggle(false);

        dailyToggle.Toggle();

        dailyToggle.IsOn.Should().Be(true);
    }

    [Fact]
    public void Toggle_WasTrue_NowIsFalse()
    {
        var dailyToggle = new DailyToggle(true);

        dailyToggle.Toggle();

        dailyToggle.IsOn.Should().Be(false);
    }

    [Fact]
    public void Toggle_WasFalseAndToggledTwice_StillIsFalse()
    {
        var dailyToggle = new DailyToggle(false);

        dailyToggle.Toggle();
        dailyToggle.Toggle();

        dailyToggle.IsOn.Should().Be(false);
    }
}
