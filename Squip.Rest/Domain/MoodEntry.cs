using System;
using NodaTime;

namespace Squip.Rest.Domain;

public class MoodEntry : DomainModelBase, IArchivable
{
    public Instant InstantOccurredAt { get; set; }
    public int Magnitude { get; set; }
    public Guid MoodId { get; set; }
    public Mood Mood { get; set; }
    public bool IsArchived { get; set; }
}
