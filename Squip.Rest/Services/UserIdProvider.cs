using Microsoft.AspNetCore.Http;

namespace Squip.Rest.Services
{
    public class UserIdProvider : IUserIdProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext!.User.Identity!.Name;
        }
    }
}
