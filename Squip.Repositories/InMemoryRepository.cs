using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Squip.Domain;

namespace Squip.Data
{
    public class InMemoryRepository<T> : IRepository<T> where T : IDomainModel
    {
        private static readonly IList<T> Entities = new List<T>();

        public Task<bool> DoesExistById(string id)
        {
            return Task.FromResult(Entities.Any(idea => idea.Id == id));
        }

        public Task<T> GetById(string id)
        {
            return Task.FromResult(Entities.SingleOrDefault(i => i.Id == id));
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return Task.FromResult(Entities.AsEnumerable());
        }

        public Task<T> Create(T t)
        {
            t.Id = Guid.NewGuid().ToString();
            Entities.Add(t);

            return Task.FromResult(t);
        }

        public Task<T> Update(T t)
        {
            return Task.FromResult(t);
        }

        public Task<bool> Archive(string id)
        {
            var entity = Entities.SingleOrDefault(e => e.Id == id);
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            Entities.Remove(entity);
            return Task.FromResult(true);
        }
    }
}
