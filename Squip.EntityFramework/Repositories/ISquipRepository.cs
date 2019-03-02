using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Pocos;

namespace Squip.EntityFramework.Repositories
{
    public interface ISquipRepository
    {
        Task<IEnumerable<SquipPoco>> GetMostRecentSquipsAsync();
    }
}
