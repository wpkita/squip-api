using System.Collections.Generic;

namespace Squip.Rest.Domain
{
    public class User : DomainModelBase
    {
        public string OidcSub { get; set; }

        public IEnumerable<Idea> Ideas { get; set; }
    }
}
