using AutoMapper;
using Squip.Rest.Domain;
using Squip.Rest.Dtos;

namespace Squip.Rest
{
    public class SquipMappingProfile : Profile
    {
        public SquipMappingProfile()
        {
            CreateMap<Idea, IdeaDto>();
        }
    }
}
