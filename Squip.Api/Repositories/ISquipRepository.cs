using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Api.DomainModels;

namespace Squip.Api.Repositories
{
    public interface ISquipRepository
    {
        Task<string> GetRandomIdeaId();
        string GetNextPresentationId();
        string GetNextReactionId();
        Task<Idea> GetIdea(string id);
        Task<Presentation> CreatePresentation(Presentation presentation);
        Task<Reaction> CreateReaction(Reaction reaction);
        Task<Idea> CreateIdea(Idea idea);
    }
}
