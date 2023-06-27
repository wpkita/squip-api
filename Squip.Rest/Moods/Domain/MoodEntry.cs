using System;
using NodaTime;
using Squip.Rest.Common.Domain;

namespace Squip.Rest.Moods.Domain;

public class MoodEntry : DomainModelBase, IArchivable
{
    public Instant InstantOccurredAt { get; set; }
    public int Magnitude { get; set; }
    public Guid MoodId { get; set; }
    public Mood Mood { get; set; }
    public bool IsArchived { get; set; }
}
