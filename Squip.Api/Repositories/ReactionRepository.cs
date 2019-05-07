using Microsoft.Extensions.Configuration;
using Squip.Api.DomainModels;

namespace Squip.Api.Repositories
{
    public class ReactionRepository : RedisRepository<Reaction>
        {
            public ReactionRepository(IConfiguration config) : base(config) { }
            protected override string entityName => "reaction";
        }
}
