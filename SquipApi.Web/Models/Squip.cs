using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquipApi.Models
{
    public class Squip
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
        
        [NotMapped]
        public IList<string> Tags { get; set; }
    }
}
