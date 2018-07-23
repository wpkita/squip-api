using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquipApi.WebApi.Models
{
    public class Squip
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
        
        public IList<SquipTag> SquipTags { get; set; }
    }

    public class SquipTag
    {
        public long SquipId { get; set; }
        public Squip Squip { get; set; }

        public string TagName { get; set; }
        public Tag Tag { get; set; }
    }

    public class Tag
    {
        public string Name { get; set; }

        public IList<SquipTag> SquipTags { get; set; }
    }
}
