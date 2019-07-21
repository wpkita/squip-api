using Microsoft.Extensions.Configuration;
using Squip.DomainModels;

namespace Squip.Repositories
{
    public class PresentationRepository : RedisRepository<Presentation>
        {
            public PresentationRepository(IConfiguration config) : base(config) { }
            protected override string entityName => "presentation";
        }
}
