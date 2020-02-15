using System.Threading.Tasks;
using Squip.Domain;

namespace Squip.Data
{
    public interface ISquipRepository
    {
        Task<Idea> GetRandomIdea();
    }
}
