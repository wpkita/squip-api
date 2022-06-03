using Microsoft.EntityFrameworkCore;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public class SquipContext : DbContext
    {
        public SquipContext(DbContextOptions options) : base(options) { }

        public DbSet<Idea> Ideas { get; set; }
    }
}
