using System.Collections.Generic;

namespace SquipApi.Models
{
    public class Squip
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
        
        public IList<string> Tags { get; set; }
    }
}
