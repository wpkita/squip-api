using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public interface IRepository<T> where T : IChangeable
    {
        Task<bool> DoesExistById(Guid id);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Create(T t);
        Task<bool> Update(T t);
        Task<bool> Archive(Guid id);
    }
}
