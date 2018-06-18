using SquipApi.Models;
using System.Collections.Generic;

namespace SquipApi.Repositories
{
    public interface ISquipRepository
    {
        IEnumerable<Squip> GetAll();
        Squip GetById(string id);
        void Add(Squip squip);
        void Remove(Squip squip);
        void Update(Squip squip);
    }
 
}
