using Microsoft.EntityFrameworkCore;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public class SquipContext : DbContext
    {
        public SquipContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Navigation(g => g.Left).AutoInclude();
            modelBuilder.Entity<Game>().Navigation(g => g.Right).AutoInclude();
        }

        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
