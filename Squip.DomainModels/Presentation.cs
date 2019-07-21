using System.Collections.Generic;

namespace Squip.DomainModels
{
    public class Presentation : DomainModelBase
    {
        public string UserId { get; set; }
        public string SquipId { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
