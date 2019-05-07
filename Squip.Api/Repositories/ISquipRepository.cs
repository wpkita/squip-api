using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Api.DomainModels;

namespace Squip.Api.Repositories
{
    public interface ISquipRepository
    {
        Task<string> GetRandomIdeaId();
        string GetNextIdeaId();
        string GetNextPresentationId();
        string GetNextReactionId();
    }
}
