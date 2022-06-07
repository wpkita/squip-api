using System;

namespace Squip.Rest.Domain
{
    public class Game : DomainModelBase
    {
        public Idea Left { get; set; }
        public Idea Right { get; set; }
        public Idea Winner { get; set; }
        public Idea Loser { get; set; }

        public void SetWinner(Idea winner)
        {
            if (Left != winner && Right != winner)
                throw new ArgumentException("Winner must be a participant in the game.");

            Winner = winner;
            Loser = Left == winner ? Right : Left;
        }
    }
}
