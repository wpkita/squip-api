using Microsoft.Extensions.Configuration;
using Squip.Domain;

namespace Squip.Data
{
    public class IdeaRepository : RedisRepository<Idea>
        {
            public IdeaRepository(IConfiguration config) : base(config) { }
            protected override string EntityName => "idea";
        }
}
