using System.Threading.Tasks;
using SquipApi.Pocos;

namespace SquipApi.Identity
{
    public interface IUserService
    {
        Task<User> GetCurrentUser();
    }
}