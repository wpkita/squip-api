using System.Collections.Generic;
using System.Linq;
using Squip.Rest.Ideas.Domain;
using Squip.Rest.Ideas.Dtos;

namespace Squip.Rest.Ideas;

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
            Tags = ideaForCreationDto.Tags == null
                ? new List<Tag>()
                : ideaForCreationDto.Tags.Select(tag => new Tag { Name = tag }).ToList()
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
}
