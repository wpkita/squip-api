using System;
using NodaTime;

namespace Squip.Rest.Dtos;

public record TargetEntryDto(Guid Id, Guid TargetId, int Magnitude, bool DidEngage, Instant InstantOccurredAt);
