using Microsoft.EntityFrameworkCore;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public class SquipContext : DbContext
    {
        public SquipContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Navigation(game => game.Left).AutoInclude();
            modelBuilder.Entity<Game>().Navigation(game => game.Right).AutoInclude();

            modelBuilder.Entity<Tag>().HasAlternateKey(tag => new { tag.IdeaId, tag.Name });
            modelBuilder.Entity<Tag>().HasIndex(tag => tag.Name);

            modelBuilder.Entity<Idea>().Navigation(idea => idea.Tags).AutoInclude();
        }

        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
