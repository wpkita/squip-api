using SquipApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SquipApi.Repositories
{
    public interface ISquipRepository
    {
        Task<IEnumerable<Squip>> GetAll();
        Task<Squip> GetById(string id);
        Task Add(Squip squip);
        Task Remove(Squip squip);
        Task Update(Squip squip);
    }
 
}
