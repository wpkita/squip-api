using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Squip.Rest.Domain;
using Squip.Rest.Services;

namespace Squip.Rest.Repositories;

public class SquipContext : DbContext
{
    private readonly IUserIdProvider _userIdProvider;

    public SquipContext(DbContextOptions options, IUserIdProvider userIdProvider)
        : base(options)
    {
        _userIdProvider = userIdProvider;
    }

    public DbSet<Idea> Ideas { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<Hibit> Hibits { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<MoodEntry> MoodEntries { get; set; }
    public DbSet<Target> Targets { get; set; }
    public DbSet<TargetEntry> TargetEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddQueryFilterToAllEntitiesAssignableFrom<IArchivable>(
            x => x.IsArchived == false
        );
        modelBuilder.AddQueryFilterToAllEntitiesAssignableFrom<IUserOwnable>(
            x => x.User.OidcSub == _userIdProvider.GetCurrentUserId()
        );

        modelBuilder.Entity<Game>().Navigation(game => game.Left).AutoInclude();
        modelBuilder.Entity<Game>().Navigation(game => game.Right).AutoInclude();

        modelBuilder.Entity<Tag>().HasAlternateKey(tag => new { tag.IdeaId, tag.Name });
        modelBuilder.Entity<Tag>().HasIndex(tag => tag.Name);

        modelBuilder.Entity<Idea>().Navigation(idea => idea.Tags).AutoInclude();
        modelBuilder.Entity<Idea>().Property(idea => idea.UserId).IsRequired();

        modelBuilder.Entity<User>().HasAlternateKey(user => user.OidcSub);
    }

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = new()
    )
    {
        var user = GetOrAddUser();

        var modifiedEntries = ChangeTracker
            .Entries()
            .Where(entry => entry.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in modifiedEntries)
        {
            var entity = entry.Entity as IChangeable;

            if (entry.State == EntityState.Added)
            {
                entity?.PreCreate();

                if (entity is IUserOwnable ownable) ownable.User = user;
            }

            entity?.PreUpdate();
        }

        var deletedEntries = ChangeTracker
            .Entries()
            .Where(entry => entry.State is EntityState.Deleted);
        foreach (var entry in deletedEntries)
        {
            if (entry.Entity is not IArchivable entity)
                continue;

            entry.State = EntityState.Modified;
            entity.IsArchived = true;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    private User GetOrAddUser()
    {
        var userId = _userIdProvider.GetCurrentUserId();
        var user = Users.SingleOrDefault(u => u.OidcSub == userId);
        if (user != null) return user;

        user = new User { OidcSub = userId };
        Users.Add(user);
        return user;
    }
}
