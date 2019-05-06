using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Api.DomainModels;

namespace Squip.Api.Repositories
{
    public interface IIdeaRepository
    {
        Task<IEnumerable<Idea>> GetIdeas();
    }
}
