using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Api.Models;

namespace Squip.Api.Repositories
{
    public interface ISquipRepository
    {
        Task<IEnumerable<SquipDto>> GetMostRecentSquipsAsync();
        Task<SquipDto> GetSquipByIdAsync(long id);
        Task<SquipDto> CreateSquipAsync(SquipDto squip);
        Task<SquipDto> UpdateSquipAsync(SquipDto squip);
        Task<SquipDto> DeleteSquipAsync(SquipDto squip);
    }
}
