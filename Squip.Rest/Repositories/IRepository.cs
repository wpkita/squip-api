using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public interface IRepository<T> where T : IDomainModel
    {
        Task<bool> DoesExistById(string id);
        Task<T> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Create(T t);
        Task<bool> Update(T t);
        Task<bool> Archive(string id);
    }
}
