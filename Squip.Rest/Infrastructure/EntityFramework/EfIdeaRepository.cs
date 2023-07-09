using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Squip.Rest.Ideas;
using Squip.Rest.Ideas.Domain;
using Squip.Rest.Infrastructure.Common;

namespace Squip.Rest.Infrastructure.EntityFramework;

public class EfIdeaRepository : IRepository<Idea>, ISquipRepository
{
    private readonly SquipContext _context;

    public EfIdeaRepository(SquipContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var idea = await _context.FindAsync<Idea>(
            new object[] { id },
            cancellationToken
        );

        return idea != null;
    }

    public async Task<Idea> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var idea = await _context.FindAsync<Idea>(
            new object[] { id },
            cancellationToken
        );

        return idea;
    }

    public async Task<IEnumerable<Idea>> GetAllAsync(CancellationToken cancellationToken)
    {
        var ideas = await _context.Ideas
            .OrderByDescending(idea => idea.EloRating)
            .ToListAsync(cancellationToken);

        return ideas;
    }

    public async Task<IEnumerable<Idea>> GetByTagAsync(string tagName, CancellationToken cancellationToken)
    {
        var ideasByTag = await _context.Tags
            .Where(tag => tag.Name == tagName)
            .Select(tag => tag.Idea)
            .Distinct()
            .OrderByDescending(idea => idea.EloRating)
            .ToListAsync(cancellationToken);

        return ideasByTag;
    }

    public async Task<bool> CreateAsync(Idea idea, CancellationToken cancellationToken)
    {
        idea.EloRating = 400;
        _context.Add(idea);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> UpdateAsync(Idea idea, CancellationToken cancellationToken)
    {
        var ideaFromDatabase = await _context.FindAsync<Idea>(
            new object[] { idea.Id },
            cancellationToken
        );
        if (ideaFromDatabase == null)
            return false;

        ideaFromDatabase.Title = idea.Title;
        ideaFromDatabase.Content = idea.Content;
        var tagsToRemove = ideaFromDatabase.Tags.Except(idea.Tags, new TagEqualityComparer());
        var tagsToAdd = idea.Tags.Except(ideaFromDatabase.Tags, new TagEqualityComparer());

        foreach (var tagToAdd in tagsToAdd) ideaFromDatabase.Tags.Add(tagToAdd);

        _context.RemoveRange(tagsToRemove);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ArchiveAsync(Guid id, CancellationToken cancellationToken)
    {
        var idea = await _context.Ideas.FindAsync(
            new object[] { id },
            cancellationToken
        );
        if (idea == null)
            return false;

        idea.IsArchived = true;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Idea> GetRandomIdeaAsync(CancellationToken cancellationToken)
    {
        var ideasArray = await _context.Ideas.ToArrayAsync(cancellationToken);
        var random = new Random();
        var randomIndex = random.Next(0, ideasArray.Length);
        return ideasArray[randomIndex];
    }

    public async Task<Tuple<Idea, Idea>> GetRandomIdeaPairAsync(
        CancellationToken cancellationToken
    )
    {
        var ideas = await _context.Ideas.ToListAsync(cancellationToken);
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
