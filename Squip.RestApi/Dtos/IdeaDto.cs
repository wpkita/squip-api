using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Squip.Domain;
using Squip.RestApi.Controllers;

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

    //public class AuthorsProfile : Profile
    //{
    //    public AuthorsProfile()
    //    {
    //        CreateMap<Entities.Author, Models.AuthorDto>()
    //            .ForMember(
    //                dest => dest.Name,
    //                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
    //            .ForMember(
    //                dest => dest.Age,
    //                opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()));

    //        CreateMap<Models.AuthorForCreationDto, Entities.Author>();
    //    }
    //}
}
