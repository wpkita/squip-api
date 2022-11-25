using System;
using System.Collections;
using System.Collections.Generic;

namespace Squip.Rest.Dtos;

public record DailySummaryDto(IEnumerable<HabitSummaryDto> HabitSummaries);
