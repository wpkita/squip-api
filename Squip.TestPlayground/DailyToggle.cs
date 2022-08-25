namespace Squip.TestPlayground;

public class DailyToggle
{
    public DailyToggle(bool isOn)
    {
        IsOn = isOn;
    }

    public bool IsOn { get; private set; }

    public void Toggle()
    {
        IsOn = !IsOn;
    }
}
