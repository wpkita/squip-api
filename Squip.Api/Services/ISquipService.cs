using System.Threading.Tasks;
using Squip.Api.Dtos;
using Squip.Api.Identity;

namespace Squip.Api.Services
{
    public interface ISquipService
    {
        Task<PresentationDto> Present(IUser user);
        Task<PresentationDto> ProcessReactionThenPresent(IUser user, ReactionDto reaction);
    }
}
