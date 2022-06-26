using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : IChangeable
    {
        private readonly IList<T> _entities = new List<T>();

        public Task<bool> DoesExistById(Guid id)
        {
            return Task.FromResult(_entities.Any(idea => idea.Id == id));
        }

        public Task<T> GetById(Guid id)
        {
            return Task.FromResult(_entities.SingleOrDefault(i => i.Id == id));
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return Task.FromResult(_entities.AsEnumerable());
        }

        public Task<bool> Create(T t)
        {
            t.Id = Guid.NewGuid();
            _entities.Add(t);

            return Task.FromResult(true);
        }

        public Task<bool> Update(T t)
        {
            return Task.FromResult(true);
        }

        public Task<bool> Archive(Guid id)
        {
            var entity = _entities.SingleOrDefault(e => e.Id == id);
            if (entity == null)
                return Task.FromResult(false);

            _entities.Remove(entity);
            return Task.FromResult(true);
        }
    }
}
