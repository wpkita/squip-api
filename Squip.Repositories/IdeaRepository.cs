using Microsoft.Extensions.Configuration;
using Squip.DomainModels;

namespace Squip.Repositories
{
    public class IdeaRepository : RedisRepository<Idea>
        {
            public IdeaRepository(IConfiguration config) : base(config) { }
            protected override string entityName => "idea";
        }
}
