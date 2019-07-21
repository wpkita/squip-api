using System.Threading.Tasks;
using Squip.Domain;

namespace Squip.Services
{
    public interface ISquipService
    {
        Task<Presentation> Inquire(Inquiry inquiry);
        Task<Presentation> React(Reaction reaction);
        Task Ideate(Idea idea);
    }
}
