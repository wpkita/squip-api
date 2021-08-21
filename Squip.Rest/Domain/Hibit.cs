namespace Squip.Rest.Domain
{
    public class Hibit : DomainModelBase
    {
        public Hibit() { }
        public Hibit(decimal score)
        {
            Score = score;
        }

        public decimal Score { get; set; }
    }
}
