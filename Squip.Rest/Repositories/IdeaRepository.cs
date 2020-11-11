using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public class IdeaRepository : RedisRepository<Idea>, ISquipRepository
    {
        public IdeaRepository(IConfiguration config) : base(config) { }
        protected override string EntityName => "idea";

        public async Task<Idea> GetRandomIdea()
        {
            var randomIdeaId = await RedisDb.SetRandomMemberAsync(ActiveEntityIdsSetName);

            var randomIdea = await GetById(randomIdeaId);

            return randomIdea;
        }
    }
}
