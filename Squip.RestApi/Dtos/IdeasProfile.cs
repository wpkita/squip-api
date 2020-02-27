using AutoMapper;
using Squip.Domain;

namespace Squip.RestApi.Dtos
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
