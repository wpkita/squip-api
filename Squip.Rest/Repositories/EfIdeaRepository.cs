using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Squip.Rest.Domain;
using System.Linq;

namespace Squip.Rest.Repositories
{
    public class EfIdeaRepository : IRepository<Idea>, ISquipRepository
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
            var ideas = await _context.Ideas
                .OrderByDescending(idea => idea.EloRating)
                .ToListAsync();

            return ideas;
        }

        public async Task<bool> Create(Idea idea)
        {
            idea.EloRating = 400;
            _context.Add(idea);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(Idea idea)
        {
            var ideaFromDatabase = await _context.FindAsync<Idea>(idea.Id);
            if (ideaFromDatabase == null)
                return false;

            ideaFromDatabase.Content = idea.Content;
            var tagsToRemove = ideaFromDatabase.Tags.Except(idea.Tags, new TagEqualityComparer());
            var tagsToAdd = idea.Tags.Except(ideaFromDatabase.Tags, new TagEqualityComparer());

            foreach (var tagToAdd in tagsToAdd)
            {
                ideaFromDatabase.Tags.Add(tagToAdd);
            }

            _context.RemoveRange(tagsToRemove);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Archive(Guid id)
        {
            var idea = await _context.Ideas.FindAsync(id);
            if (idea == null)
                return false;

            idea.IsArchived = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Idea> GetRandomIdea()
        {
            var ideasArray = await _context.Ideas.ToArrayAsync();
            var random = new Random();
            var randomIndex = random.Next(0, ideasArray.Length);
            return ideasArray[randomIndex];
        }

        public async Task<Tuple<Idea, Idea>> GetRandomIdeaPair()
        {
            var ideas = await _context.Ideas.ToListAsync();
            if (ideas.Count < 2)
                return new Tuple<Idea, Idea>(new Idea(), new Idea());

            var random = new Random();

            var randomIndex = random.Next(0, ideas.Count);
            var firstIdea = ideas[randomIndex];
            ideas.Remove(firstIdea);

            randomIndex = random.Next(0, ideas.Count);
            var secondIdea = ideas[randomIndex];

            return new Tuple<Idea, Idea>(firstIdea, secondIdea);
        }
    }
}
