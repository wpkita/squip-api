using System;
using System.Threading;
using System.Threading.Tasks;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories;

public interface ISquipRepository
{
    Task<Idea> GetRandomIdeaAsync(CancellationToken cancellationToken);
    Task<Tuple<Idea, Idea>> GetRandomIdeaPairAsync(CancellationToken cancellationToken);
}
