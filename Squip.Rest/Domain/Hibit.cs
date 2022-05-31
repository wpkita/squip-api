namespace Squip.Rest.Domain
{
    public class Hibit : DomainModelBase
    {
        public Hibit() { }

        public Hibit(decimal score)
        {
            Score = score;
        }

        public Habit Habit { get; set; }

        public decimal Score { get; set; }
    }
}
