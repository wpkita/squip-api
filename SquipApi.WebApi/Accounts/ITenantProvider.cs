using System.Linq;

namespace SquipApi.WebApi
{
    public interface ITenantProvider
    {
        string TenantId { get; }
    }
}
