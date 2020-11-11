using AutoMapper;
using Squip.Rest.Domain;

namespace Squip.Rest.Dtos
{
    public class IdeasProfile : Profile
    {
        public IdeasProfile()
        {
            CreateMap<Idea, IdeaDto>();
            CreateMap<IdeaForCreationDto, Idea>();
        }
    }
}
