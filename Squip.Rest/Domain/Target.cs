using System.Collections.Generic;

namespace Squip.Rest.Domain;

public class Target : DomainModelBase, IArchivable
{
    public string Name { get; set; }
    public bool IsArchived { get; set; }
    public ICollection<TargetEntry> TargetEntries { get; set; }
}
