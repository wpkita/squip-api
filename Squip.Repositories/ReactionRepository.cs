using Microsoft.Extensions.Configuration;
using Squip.Domain;

namespace Squip.Data
{
    public class ReactionRepository : RedisRepository<Reaction>
        {
            public ReactionRepository(IConfiguration config) : base(config) { }
            protected override string entityName => "reaction";
        }
}
