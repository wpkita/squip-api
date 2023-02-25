using System;
using NodaTime;

namespace Squip.Rest.Domain;

public class TargetEntry : DomainModelBase, IArchivable
{
    public Instant InstantOccurredAt { get; set; }
    public int Magnitude { get; set; }
    public bool DidEngage { get; set; }
    public Guid TargetId { get; set; }
    public Target Target { get; set; }
    public bool IsArchived { get; set; }
}
