using System.Collections.Generic;

namespace Squip.Pocos
{
    public class TagPoco : BasePoco
    {
        public long SquipId { get; set; }
        public string Name { get; set; }

        public SquipPoco SquipPoco { get; set; }
    }
}
