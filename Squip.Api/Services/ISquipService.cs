using System.Threading.Tasks;
using Squip.Api.Dtos;

namespace Squip.Api.Services
{
    public interface ISquipService
    {
        Task<PresentationDto> Present();
        Task<PresentationDto> ProcessReactionThenPresent(ReactionDto reaction);
    }
}
