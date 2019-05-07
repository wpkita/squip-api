using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Api.DomainModels;

namespace Squip.Api.Repositories
{
    public interface IRepository<T> where T : IDomainModel
    {
        Task<bool> DoesExistById(string id);
        Task<T> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Create(T t);
        Task<T> Update(T t);
    }
}
