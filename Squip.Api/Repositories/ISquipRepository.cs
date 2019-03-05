using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Api.Models;

namespace Squip.Api.Repositories
{
    public interface ISquipRepository
    {
        Task<SquipDto> GetSquip();
    }
}
