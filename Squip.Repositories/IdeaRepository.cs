using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Squip.Domain;

namespace Squip.Data
{
    public class IdeaRepository : RedisRepository<Idea>, ISquipRepository
    {
        public IdeaRepository(IConfiguration config) : base(config) { }
        protected override string EntityName => "idea";

        public async Task<Idea> GetRandomIdea()
        {
            var randomIdeaId = await _redisDb.SetRandomMemberAsync(ActiveEntityIdsSetName);

            var randomIdea = await GetById(randomIdeaId);

            return randomIdea;
        }
    }
}
