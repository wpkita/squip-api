using System.Linq;
using Squip.Rest.Domain;

namespace Squip.Rest.Dtos;

public class IdeasProfile
{
    public static GameDto MapGameToDto(Game game)
    {
        return new GameDto(game.Id, MapIdeaToDto(game.Left), MapIdeaToDto(game.Right));
    }

    public static IdeaDto MapIdeaToDto(Idea idea)
    {
        return new IdeaDto(
            idea.Id,
            idea.Title,
            idea.Content,
            idea.Tags.Select(tag => tag.Name).ToArray()
        );
    }

    public static Idea MapDtoToIdea(IdeaForCreationDto ideaForCreationDto)
    {
        return new Idea
        {
            Title = ideaForCreationDto.Title,
            Content = ideaForCreationDto.Content,
            Tags = ideaForCreationDto.Tags.Select(tag => new Tag { Name = tag }).ToList()
        };
    }

    public static Idea MapDtoToIdea(IdeaDto ideaDto)
    {
        return new Idea
        {
            Id = ideaDto.Id,
            Title = ideaDto.Title,
            Content = ideaDto.Content,
            Tags = ideaDto.Tags.Select(tag => new Tag { Name = tag }).ToList()
        };
    }

    public static Habit MapDtoToHabit(HabitDto habitDto)
    {
        return new Habit
        {
            Id = habitDto.Id,
            Name = habitDto.Name
        };
    }

    public static Habit MapDtoToHabit(HabitForCreationDto habitForCreationDto)
    {
        return new Habit
        {
            Name = habitForCreationDto.Name
        };
    }

    public static HabitDto MapHabitToDto(Habit habit)
    {
        return new HabitDto(habit.Id, habit.Name);
    }

    public static Hibit MapDtoToHibit(HibitDto hibitDto)
    {
        return new Hibit
        {
            Id = hibitDto.Id,
            HabitId = hibitDto.HabitId,
            InstantCreatedAt = hibitDto.InstantOccurredAt
        };
    }

    public static Hibit MapDtoToHibit(HibitForCreationDto hibitForCreationDto)
    {
        return new Hibit
        {
            HabitId = hibitForCreationDto.HabitId
        };
    }

    public static HibitDto MapHibitToDto(Hibit hibit)
    {
        return new HibitDto(hibit.Id, hibit.HabitId, hibit.InstantOccurredAt);
    }
}
