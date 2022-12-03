using System;

namespace Squip.Rest.Dtos;

public record HabitSummaryDto(HabitDto Habit, int Count);
