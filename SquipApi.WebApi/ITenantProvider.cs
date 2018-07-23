using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace SquipApi.WebApi
{
    public interface ITenantProvider
    {
        string TenantId { get; }
    }

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
