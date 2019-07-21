using Microsoft.Extensions.Configuration;
using Squip.DomainModels;

namespace Squip.Repositories
{
    public class ReactionRepository : RedisRepository<Reaction>
        {
            public ReactionRepository(IConfiguration config) : base(config) { }
            protected override string entityName => "reaction";
        }
}
