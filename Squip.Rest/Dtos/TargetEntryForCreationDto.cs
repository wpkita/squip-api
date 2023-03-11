using System;

namespace Squip.Rest.Dtos;

public record TargetEntryForCreationDto(Guid TargetId, int Magnitude, bool DidEngage);
