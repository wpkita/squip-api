using System.Collections.Generic;
using SquipApi.Pocos;

namespace SquipApi.Pocos
{
    public class Tag : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<SquipTag> SquipTags { get; set; }
    }
}
