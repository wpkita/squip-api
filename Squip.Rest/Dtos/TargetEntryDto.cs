using System;

namespace Squip.Rest.Dtos;

public record TargetEntryDto(Guid TargetId, int Magnitude, bool DidEngage);
