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
}
