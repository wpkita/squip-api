using System;
using System.Collections.Generic;

namespace Squip.Rest.Domain
{
    public class Idea : DomainModelBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsArchived { get; set; }
        public double EloRating { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
