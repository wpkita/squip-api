using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Squip.Rest.Domain;
using Squip.Rest.Services;

namespace Squip.Rest.Repositories
{
    public class SquipContext : DbContext
    {
        private readonly IUserIdProvider _userIdProvider;

        public SquipContext(DbContextOptions options, IUserIdProvider userIdProvider)
            : base(options)
        {
            _userIdProvider = userIdProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Navigation(game => game.Left).AutoInclude();
            modelBuilder.Entity<Game>().Navigation(game => game.Right).AutoInclude();

            modelBuilder.Entity<Tag>().HasAlternateKey(tag => new { tag.IdeaId, tag.Name });
            modelBuilder.Entity<Tag>().HasIndex(tag => tag.Name);

            modelBuilder.Entity<Idea>().Navigation(idea => idea.Tags).AutoInclude();
            modelBuilder.Entity<Idea>().HasQueryFilter(idea => !idea.IsArchived);
            modelBuilder
                .Entity<Idea>()
                .HasQueryFilter(idea => idea.User.OidcSub == _userIdProvider.GetCurrentUserId());
            modelBuilder.Entity<Idea>().Property(idea => idea.UserId).IsRequired();

            modelBuilder.Entity<User>().HasAlternateKey(user => user.OidcSub);
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = new CancellationToken()
        )
        {
            var modifiedEntries = ChangeTracker
                .Entries()
                .Where(entry => entry.State is EntityState.Added or EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as DomainModelBase;

                if (entry.State == EntityState.Added)
                {
                    entity?.PreCreate();
                }

                entity?.PreUpdate();
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
