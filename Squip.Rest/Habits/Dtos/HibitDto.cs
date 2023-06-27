using System;
using NodaTime;

namespace Squip.Rest.Habits.Dtos;

public record HibitDto(Guid Id, Guid HabitId, Instant InstantOccurredAt);
