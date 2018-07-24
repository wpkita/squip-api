using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SquipApi.WebApi
{
    public class HttpContextTenantProvider : ITenantProvider
    {
        public HttpContextTenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            TenantId = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

        public string TenantId { get; }
    }
}
