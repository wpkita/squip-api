using System.Threading.Tasks;
using Squip.DomainModels;
using Squip.Api.Identity;

namespace Squip.Api.Services
{
    public interface ISquipService
    {
        Task<Presentation> Inquire(Inquiry inquiry);
        Task<Presentation> React(Reaction reaction);
        Task Ideate(Idea idea);
    }
}
