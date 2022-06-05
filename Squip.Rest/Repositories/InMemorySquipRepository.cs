using System;
using System.Threading.Tasks;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public class InMemorySquipRepository : ISquipRepository
    {
        public Task<Idea> GetRandomIdea()
        {
            return Task.FromResult(new Idea { Id = Guid.NewGuid(), Content = "Hello world!" });
        }
    }
}
