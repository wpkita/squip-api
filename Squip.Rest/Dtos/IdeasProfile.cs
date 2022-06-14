using System.Linq;
using AutoMapper;
using Squip.Rest.Domain;

namespace Squip.Rest.Dtos
{
    public class IdeasProfile : Profile
    {
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

        public IdeasProfile()
        {
            CreateMap<Idea, IdeaDto>()
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Name))
                )
                .ReverseMap()
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Select(t => new Tag { Name = t }))
                );
        }
    }
}
