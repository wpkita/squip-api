namespace Squip.TestPlayground
{
    public class DailyCounter
    {
        public DailyCounter(int value = 0)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public void Activate()
        {
            Value += 1;
        }
    }
}