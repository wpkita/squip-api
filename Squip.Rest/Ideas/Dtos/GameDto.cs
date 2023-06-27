using System;

namespace Squip.Rest.Ideas.Dtos;

public record GameDto(Guid Id, IdeaDto Left, IdeaDto Right);
