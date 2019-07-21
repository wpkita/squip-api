using System.Threading.Tasks;

namespace Squip.Repositories
{
    public interface ISquipRepository
    {
        Task<string> GetRandomIdeaId();
        string GetNextIdeaId();
        string GetNextPresentationId();
        string GetNextReactionId();
    }
}
