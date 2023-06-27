using System;

namespace Squip.Rest.Ideas.Dtos;

public record IdeaDto(Guid Id, string Title, string Content, string[] Tags);
