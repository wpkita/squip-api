using Squip.Rest.Habits.Domain;
using Squip.Rest.Habits.Dtos;

namespace Squip.Rest.Habits;

public class HabitsProfile
{
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
