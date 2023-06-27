using System;

namespace Squip.Rest.Targets.Dtos;

public record TargetEntryForCreationDto(Guid TargetId, int Magnitude, bool DidEngage);
