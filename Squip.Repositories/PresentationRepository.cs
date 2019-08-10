using Microsoft.Extensions.Configuration;
using Squip.Domain;

namespace Squip.Data
{
    public class PresentationRepository : RedisRepository<Presentation>
        {
            public PresentationRepository(IConfiguration config) : base(config) { }
            protected override string EntityName => "presentation";
        }
}
