using System;
using AutoMapper;
using Squip.Domain;

namespace Squip.RestApi.Dtos
{
    public class IdeaDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
    }

    public class IdeasProfile : Profile
    {
        public IdeasProfile()
        {
            CreateMap<Idea, IdeaDto>();
            CreateMap<IdeaForCreationDto, Idea>();
        }
    }
}
