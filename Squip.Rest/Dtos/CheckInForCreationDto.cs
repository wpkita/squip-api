using System.Collections;
using System.Collections.Generic;

namespace Squip.Rest.Dtos;

public record CheckInForCreationDto(IEnumerable<MoodEntryDto> MoodEntries);
