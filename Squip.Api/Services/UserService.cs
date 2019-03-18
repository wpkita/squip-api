using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Squip.Api.Identity;

namespace Squip.Api.Services
{
    public class UserService : IUserService
    {
        private const string FirebaseUserIdKey = "user_id";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IUser GetCurrentUser()
        {
            IUser user = null;

            var claimsPrincipal = _httpContextAccessor.HttpContext.User as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                var userId = claimsPrincipal.Claims.SingleOrDefault(c => c.Type == FirebaseUserIdKey)?.Value;
                user = new User
                {
                    Id = userId
                };
            }

            return user;
        }
    }
}
