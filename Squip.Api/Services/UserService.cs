using Microsoft.AspNetCore.Http;
using Squip.Api.Identity;
using System.Linq;
using System.Security.Claims;

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
            if (!(_httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity claimsPrincipal)) return null;

            var userId = claimsPrincipal.Claims.SingleOrDefault(c => c.Type == UserIdClaimType)?.Value;
            var user = new User
            {
                Id = userId
            };

            return user;
        }
    }
}
