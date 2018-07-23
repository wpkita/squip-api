using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SquipApi.WebApi.Dtos;
using SquipApi.WebApi.Models;

namespace SquipApi.WebApi.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Squip, SquipDto>().ForMember(dto => dto.Tags,
                orig => orig.MapFrom(src => src.SquipTags.Select(st => st.Tag.Name)));
        }
    }
}
