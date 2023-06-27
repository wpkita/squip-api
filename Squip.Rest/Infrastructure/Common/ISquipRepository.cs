using System;
using System.Threading;
using System.Threading.Tasks;
using Squip.Rest.Ideas.Domain;

namespace Squip.Rest.Infrastructure.Common;

public interface ISquipRepository
{
    Task<Idea> GetRandomIdeaAsync(CancellationToken cancellationToken);
    Task<Tuple<Idea, Idea>> GetRandomIdeaPairAsync(CancellationToken cancellationToken);
}
