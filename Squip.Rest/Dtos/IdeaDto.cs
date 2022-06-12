using System;
using Squip.Rest.Domain;

namespace Squip.Rest.Dtos
{
    public record IdeaDto(Guid Id, string Title, string Content, string[] Tags);
}
