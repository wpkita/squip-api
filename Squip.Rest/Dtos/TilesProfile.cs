using AutoMapper;
using Squip.Rest.Domain;

namespace Squip.Rest.Dtos
{
    public class TilesProfile : Profile
    {
        public TilesProfile()
        {
            CreateMap<Tile, TileDto>();
            CreateMap<TileForCreationDto, Tile>();
        }
    }
}
