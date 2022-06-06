namespace Squip.Rest.Domain
{
    public class Game : DomainModelBase
    {
        public Idea Left { get; set; }
        public Idea Right { get; set; }
        public Idea Winner { get; set; }
        public Idea Loser { get; set; }
    }
}
