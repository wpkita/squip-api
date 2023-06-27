using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Squip.Rest.Common.Domain;

namespace Squip.Rest.Infrastructure.Common;

public interface IRepository<T> where T : IChangeable
{
    Task<bool> DoesExistByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> CreateAsync(T t, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T t, CancellationToken cancellationToken);
    Task<bool> ArchiveAsync(Guid id, CancellationToken cancellationToken);
}
