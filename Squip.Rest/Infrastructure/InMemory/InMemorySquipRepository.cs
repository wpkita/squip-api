using System;
using System.Threading;
using System.Threading.Tasks;
using Squip.Rest.Ideas.Domain;
using Squip.Rest.Infrastructure.Common;

namespace Squip.Rest.Infrastructure.InMemory;

public class InMemorySquipRepository : ISquipRepository
{
    public Task<Idea> GetRandomIdeaAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new Idea { Id = Guid.NewGuid(), Content = "Hello world!" });
    }

    public Task<Tuple<Idea, Idea>> GetRandomIdeaPairAsync(string filter, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            new Tuple<Idea, Idea>(
                GetRandomIdeaAsync(cancellationToken).Result,
                GetRandomIdeaAsync(cancellationToken).Result
            )
        );
    }
}
