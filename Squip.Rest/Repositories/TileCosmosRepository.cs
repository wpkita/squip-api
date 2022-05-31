using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public class TileCosmosRepository : CosmosRepository<Tile>
    {
        public TileCosmosRepository(IConfiguration configuration, ILogger logger)
            : base(configuration, logger) { }

        protected override string ContainerName => "tiles";
    }
}
