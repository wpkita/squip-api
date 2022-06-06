using System;

namespace Squip.Rest.Dtos
{
    public record GameDto(Guid Id, IdeaDto Left, IdeaDto Right);
}
