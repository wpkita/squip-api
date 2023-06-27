using System.Collections.Generic;

namespace Squip.Rest.Moods.Dtos;

public record CheckInForCreationDto(IEnumerable<MoodEntryDto> MoodEntries);
