using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Squip.Rest.Ideas.Domain;
using Squip.Rest.Infrastructure.Common;

namespace Squip.Rest.Infrastructure.Redis;

public class IdeaRepository : RedisRepository<Idea>, ISquipRepository
{
    public IdeaRepository(IConfiguration config) : base(config)
    {
    }

    protected override string EntityName => "idea";

    public async Task<Idea> GetRandomIdeaAsync(CancellationToken cancellationToken)
    {
        var randomIdeaId = await RedisDb.SetRandomMemberAsync(ActiveEntityIdsSetName);

        var randomIdea = await GetByIdAsync(Guid.Parse(randomIdeaId), cancellationToken);

        return randomIdea;
    }

    public Task<Tuple<Idea, Idea>> GetRandomIdeaPairAsync(string filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
