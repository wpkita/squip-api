using AutoMapper;
using Squip.Pocos;
using System.Linq;

namespace Squip.Api.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SquipDto, SquipPoco>();
            CreateMap<SquipPoco, SquipDto>();

        }
    }
}
