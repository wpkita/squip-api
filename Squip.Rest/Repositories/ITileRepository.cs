using System;
using System.Collections.Generic;
using System.Linq;
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

    public class InMemoryTileRepository : ITileRepository
    {
        private readonly IList<Tile> tiles;

        public InMemoryTileRepository()
        {
            tiles = new List<Tile>();
        }
        public Task<Tile> GetById(string id)
        {
            var tileFromDb = tiles.SingleOrDefault(t => t.Id == id);

            return Task.FromResult(tileFromDb);
        }

        public Task<IEnumerable<Tile>> GetAll()
        {
            return Task.FromResult(tiles.AsEnumerable());
        }

        public Task<Tile> Create(Tile tile)
        {
            tile.Id = Guid.NewGuid().ToString();

            tiles.Add(tile);

            return Task.FromResult(tile);
        }

        public Task<Tile> Update(Tile tile)
        {
            var tileFromDb = tiles.SingleOrDefault(t => t.Id == tile.Id);

            tileFromDb.Name = tile.Name;
            tileFromDb.Type = tile.Type;

            return Task.FromResult(tileFromDb);
        }

        public Task<bool> Delete(Tile tile)
        {
            var tileFromDb = tiles.Single(t => t.Id == tile.Id);

            tiles.Remove(tileFromDb);

            return Task.FromResult(true);
        }
    }
}
