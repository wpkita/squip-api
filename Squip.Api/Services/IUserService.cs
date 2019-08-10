using Squip.Api.Identity;

namespace Squip.Api.Services
{
    public interface IUserService
    {
        IUser GetCurrentUser();
    }
}
