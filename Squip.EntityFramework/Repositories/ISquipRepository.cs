using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Pocos;

namespace Squip.EntityFramework.Repositories
{
    public interface ISquipRepository
    {
        Task<IEnumerable<SquipPoco>> GetMostRecentSquipsAsync();
        Task<SquipPoco> GetSquipByIdAsync(long id);
        Task<SquipPoco> CreateSquipAsync(SquipPoco squip);
        Task<SquipPoco> UpdateSquipAsync(SquipPoco squip);
        Task<SquipPoco> DeleteSquipAsync(SquipPoco squip);
    }
}
