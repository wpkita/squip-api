using System.Threading.Tasks;

namespace Squip.Data
{
    public interface ISquipRepository
    {
        Task<string> GetRandomIdeaId();
        string GetNextIdeaId();
        string GetNextPresentationId();
        string GetNextReactionId();
    }
}
