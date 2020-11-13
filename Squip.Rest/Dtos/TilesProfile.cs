using AutoMapper;
using Squip.Rest.Domain;

namespace Squip.Rest.Dtos
{
    public class TilesProfile : Profile
    {
        public TilesProfile()
        {
            CreateMap<TileDto, Tile>();
            CreateMap<Tile, TileDto>();
            CreateMap<TileForCreationDto, Tile>();
        }
    }
}
