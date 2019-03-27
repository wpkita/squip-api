using System.Threading.Tasks;
using Squip.Api.Dtos;
using Squip.Api.Identity;

namespace Squip.Api.Services
{
    public interface ISquipService
    {
        Task<PresentationDto> Present(IUser user);
        Task<PresentationDto> React(IUser user, ReactionDto reaction);
        Task<ValidationDto> Ideate(IUser user, IdeaDto ideaDto);
    }
}
