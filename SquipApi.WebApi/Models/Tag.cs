using System.Collections.Generic;

namespace SquipApi.WebApi.Models
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<SquipTag> SquipTags { get; set; }
    }
}
