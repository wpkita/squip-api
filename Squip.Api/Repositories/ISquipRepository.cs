using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Api.Dtos;
using Squip.Api.Secrets;

namespace Squip.Api.Repositories
{



    public interface ISquipRepository
    {
        Task<IdeaSecret> GetIdea();
        Task<PresentationSecret> AddPresentation(PresentationSecret presentation);
        Task<ReactionSecret> AddReaction(ReactionSecret reaction);
        Task<IdeaSecret> AddIdea(IdeaSecret idea);
    }
}
