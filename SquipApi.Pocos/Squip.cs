using System.Collections.Generic;

namespace SquipApi.Pocos
{
    public class Squip : BaseEntity
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
        
        public IList<SquipTag> SquipTags { get; set; }
    }
}
