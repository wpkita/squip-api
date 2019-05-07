using Microsoft.Extensions.Configuration;
using Squip.Api.DomainModels;

namespace Squip.Api.Repositories
{
    public class IdeaRepository : RedisRepository<Idea>
        {
            public IdeaRepository(IConfiguration config) : base(config) { }
            protected override string entityName => "idea";
        }
}
