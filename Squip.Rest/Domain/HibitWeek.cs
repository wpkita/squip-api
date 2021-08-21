using System;
using System.Collections.Generic;
using System.Linq;

namespace Squip.Rest.Domain
{
    public class HibitWeek : DomainModelBase
    {
        public IList<Hibit> Hibits { get; set; }
        public decimal ScoreGoal { get; set; }

        public decimal RawScore
        {
            get { return Math.Min(ScoreGoal, Hibits.Sum(h => h.Score)); }
        }

        public decimal NormalizedScore => RawScore / ScoreGoal;
        public decimal ScoreWeight { get; set; }
    }
}
