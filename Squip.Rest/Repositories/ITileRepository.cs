using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    interface ITileRepository
    {
        Task<Tile> GetById(string id);
        Task<IEnumerable<Tile>> GetAll();
        Task<Tile> Create(Tile tile);
        Task<Tile> Update(Tile tile);
        Task<bool> Delete(Tile tile);
    }
}
