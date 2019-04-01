using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Squip.Api.Identity;

namespace Squip.Api.Services
{
    public class OktaUserService : IUserService
    {
        private const string UserIdClaimType = "uid";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OktaUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IUser GetCurrentUser()
        {
            IUser user = null;

            var claimsPrincipal = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (claimsPrincipal != null)
            {
                var userId = claimsPrincipal.Claims.SingleOrDefault(c => c.Type == UserIdClaimType)?.Value;
                user = new User
                {
                    Id = userId
                };
            }

            return user;
        }
    }
}
