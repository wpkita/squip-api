using System.Collections.Generic;
using System.Linq;

namespace Squip.Rest.Domain
{
    public class Week : DomainModelBase
    {
        public List<HibitWeek> HibitWeeks { get; set; }

        public decimal Score => HibitWeeks.Sum(hw => hw.NormalizedScore * hw.ScoreWeight);
    }
}
