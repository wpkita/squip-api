using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public class EfIdeaRepository : IRepository<Idea>
    {
        private readonly SquipContext _context;

        public EfIdeaRepository(SquipContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesExistById(Guid id)
        {
            var idea = await _context.FindAsync<Idea>(id);

            return idea != null;
        }

        public async Task<Idea> GetById(Guid id)
        {
            var idea = await _context.FindAsync<Idea>(id);

            return idea;
        }

        public async Task<IEnumerable<Idea>> GetAll()
        {
            var ideas = await _context.Ideas.ToListAsync();

            return ideas;
        }

        public async Task<bool> Create(Idea idea)
        {
            idea.PreCreate();
            _context.Add(idea);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> Update(Idea t)
        {
            t.PreUpdate();
            throw new System.NotImplementedException();
        }

        public Task<bool> Archive(Guid id)
        {
            throw new System.NotImplementedException();
        }
    }
}
