using System;
using NodaTime;
using Squip.Rest.Common.Domain;

namespace Squip.Rest.Habits.Domain;

public class Hibit : DomainModelBase, IArchivable
{
    public Instant InstantOccurredAt { get; set; }
    public Guid HabitId { get; set; }
    public Habit Habit { get; set; }
    public bool IsArchived { get; set; }
}
