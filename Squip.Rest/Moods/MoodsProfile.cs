using Squip.Rest.Moods.Domain;
using Squip.Rest.Moods.Dtos;

namespace Squip.Rest.Moods;

public class MoodsProfile
{
    public static Mood MapDtoToMood(MoodForCreationDto moodForCreationDto)
    {
        return new Mood
        {
            Name = moodForCreationDto.Name
        };
    }

    public static MoodDto MapMoodToDto(Mood mood)
    {
        return new MoodDto(mood.Id, mood.Name);
    }

    public static MoodEntry MapDtoToMoodEntry(MoodEntryDto moodEntryDto)
    {
        return new MoodEntry
        {
            MoodId = moodEntryDto.MoodId,
            Magnitude = moodEntryDto.Magnitude
        };
    }
}
