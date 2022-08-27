using System;
using NodaTime;

namespace Squip.Rest.Dtos;

public record HibitDto(Guid Id, Guid HabitId, Instant InstantOccurredAt);
