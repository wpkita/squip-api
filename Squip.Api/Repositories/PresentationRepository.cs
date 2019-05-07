using Microsoft.Extensions.Configuration;
using Squip.Api.DomainModels;

namespace Squip.Api.Repositories
{
    public class PresentationRepository : RedisRepository<Presentation>
        {
            public PresentationRepository(IConfiguration config) : base(config) { }
            protected override string entityName => "presentation";
        }
}
