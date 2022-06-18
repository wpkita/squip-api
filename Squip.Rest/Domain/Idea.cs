using System.Collections.Generic;

namespace Squip.Rest.Domain
{
    public class Idea : DomainModelBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public bool IsArchived { get; set; }
        public double EloRating { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
