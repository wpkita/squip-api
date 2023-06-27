using Squip.Rest.Targets.Domain;
using Squip.Rest.Targets.Dtos;

namespace Squip.Rest.Targets;

public class TargetsProfile
{
    public static TargetDto MapTargetToDto(Target target)
    {
        return new TargetDto(target.Id, target.Name);
    }

    public static Target MapDtoToTarget(TargetForCreationDto targetForCreationDto)
    {
        return new Target
        {
            Name = targetForCreationDto.Name
        };
    }

    public static TargetEntry MapDtoToTargetEntry(TargetEntryForCreationDto targetEntryForCreationDto)
    {
        return new TargetEntry
        {
            TargetId = targetEntryForCreationDto.TargetId,
            Magnitude = targetEntryForCreationDto.Magnitude,
            DidEngage = targetEntryForCreationDto.DidEngage
        };
    }

    public static TargetEntryDto MapTargetEntryToDto(TargetEntry targetEntry)
    {
        return new TargetEntryDto(targetEntry.Id, targetEntry.TargetId, targetEntry.Magnitude, targetEntry.DidEngage,
            targetEntry.InstantOccurredAt);
    }
}
