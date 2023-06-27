using System.Collections.Generic;
using Squip.Rest.Common.Domain;

namespace Squip.Rest.Targets.Domain;

public class Target : DomainModelBase, IArchivable
{
    public string Name { get; set; }
    public bool IsArchived { get; set; }
    public ICollection<TargetEntry> TargetEntries { get; set; }
}
