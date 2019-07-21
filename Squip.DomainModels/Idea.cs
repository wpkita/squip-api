using System.Collections.Generic;

namespace Squip.Domain
{
    public class Idea : DomainModelBase
    {
        public virtual string Content { get; set; }
        public virtual IEnumerable<string> Tags { get; set; }
        public virtual string UserId { get; set; }
    }
}
