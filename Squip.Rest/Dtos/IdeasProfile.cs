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

    public static Idea MapDtoToIdea(IdeaForCreationDto idea)
    {
        return new Idea
        {
            Title = idea.Title,
            Content = idea.Content,
            Tags = idea.Tags.Select(tag => new Tag { Name = tag }).ToList()
        };
    }

    public static Idea MapDtoToIdea(IdeaDto idea)
    {
        return new Idea
        {
            Id = idea.Id,
            Title = idea.Title,
            Content = idea.Content,
            Tags = idea.Tags.Select(tag => new Tag { Name = tag }).ToList()
        };
    }

    public static Habit MapDtoToHabit(HabitDto habit)
    {
        return new Habit
        {
            Id = habit.Id,
            Name = habit.Name
        };
    }

    public static Habit MapDtoToHabit(HabitForCreationDto habit)
    {
        return new Habit
        {
            Name = habit.Name
        };
    }

    public static HabitDto MapHabitToDto(Habit habit)
    {
        return new HabitDto(habit.Id, habit.Name);
    }
}
