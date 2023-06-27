using System.Collections.Generic;
using Squip.Rest.Common.Domain;

namespace Squip.Rest.Ideas.Domain;

public class Idea : DomainModelBase, IArchivable
{
    public string Title { get; set; }
    public string Content { get; set; }
    public double EloRating { get; set; }
    public ICollection<Tag> Tags { get; set; }
    public bool IsArchived { get; set; }
}
