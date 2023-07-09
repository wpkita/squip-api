using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Squip.Rest.Common.Domain;
using Squip.Rest.Infrastructure.Common;

namespace Squip.Rest.Infrastructure.InMemory;

public class InMemoryRepository<T> : IRepository<T> where T : IChangeable
{
    private readonly IList<T> _entities = new List<T>();

    public Task<bool> DoesExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_entities.Any(idea => idea.Id == id));
    }

    public Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_entities.SingleOrDefault(i => i.Id == id));
    }

    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_entities.AsEnumerable());
    }

    public Task<IEnumerable<T>> GetByTagAsync(string tagName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateAsync(T t, CancellationToken cancellationToken)
    {
        t.Id = Guid.NewGuid();
        _entities.Add(t);

        return Task.FromResult(true);
    }

    public Task<bool> UpdateAsync(T t, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }

    public Task<bool> ArchiveAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = _entities.SingleOrDefault(e => e.Id == id);
        if (entity == null)
            return Task.FromResult(false);

        _entities.Remove(entity);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<string>> GetAllTagsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
