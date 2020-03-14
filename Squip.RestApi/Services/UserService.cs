using System;
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
            if (!(_httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity claimsIdentity)) return null;

            var userId = GetUserIdFromClaimsIdentity(claimsIdentity);
            var user = new User
            {
                Id = userId
            };

            return user;
        }

        public string GetUserIdFromClaimsIdentity(ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null)
            {
                throw new ArgumentNullException(nameof(claimsIdentity));
            }

            return claimsIdentity.Claims.SingleOrDefault(c => c.Type == UserIdClaimType)?.Value;
        }
    }
}
