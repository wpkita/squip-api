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

            CreateMap<SquipPoco, SquipSummaryDto>().ForMember(dto => dto.Tags, opt => opt.MapFrom(s => s.TagPocos.Select(st => st.Name)));
        }
    }
}
