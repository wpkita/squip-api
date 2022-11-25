using System;

namespace Squip.Rest.Dtos;

public record HabitSummaryDto(Guid Id, string Name, int Count);
