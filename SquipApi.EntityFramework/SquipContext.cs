using Microsoft.EntityFrameworkCore;
using SquipApi.Identity;
using SquipApi.Pocos;

namespace SquipApi.EntityFramework
{
    public class SquipContext : DbContext
    {
        private readonly IUserService _userService;

        public SquipContext(DbContextOptions<SquipContext> options, IUserService userService)
            : base(options)
        {
            _userService = userService;
        }

        public SquipContext(DbContextOptions<SquipContext> options)
            : base(options)
        {
        }

        public DbSet<Squip> Squips { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Tag> SquipTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("User");
                b.HasKey(u => u.Id);
                b.HasAlternateKey(u => u.ThirdPartyId);
            });

            modelBuilder.Entity<Squip>(b =>
            {
                b.ToTable("Squip");
                b.HasKey(s => s.Id);
                b.Property(s => s.Title).IsRequired();
                b.Property(s => s.Body).IsRequired();
                b.HasQueryFilter(s => !s.IsSoftDeleted);
            });

            modelBuilder.Entity<Tag>(b =>
            {
                b.ToTable("Tag");
                b.HasKey(t => t.Id);
                b.HasAlternateKey(t => t.Name);
                b.HasQueryFilter(t => !t.IsSoftDeleted);
            });

            modelBuilder.Entity<SquipTag>(b =>
            {
                b.ToTable("SquipTag");
                b.HasKey(k => k.Id);
                b.HasAlternateKey(k => new { k.TagId, k.SquipId });
                b.HasOne(st => st.Squip).WithMany(s => s.SquipTags)
                    .HasForeignKey(st => st.SquipId);
                b.HasOne(st => st.Tag).WithMany(t => t.SquipTags)
                    .HasForeignKey(st => st.TagId);
                b.HasQueryFilter(st => !st.IsSoftDeleted);
            });
        }

        public override int SaveChanges()
        {
            var changedEntities = ChangeTracker.Entries();
            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is BaseEntity entity)
                {
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.OnBeforeInsert();
                        // TODO: Use await here!
                        entity.CreatedByUserId = _userService.GetCurrentUser().Result.Id;
                        entity.ModifiedByUserId = _userService.GetCurrentUser().Result.Id;
                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.OnBeforeUpdate();
                        // TODO: Use await here!
                        entity.ModifiedByUserId = _userService.GetCurrentUser().Result.Id;
                    }
                    else if
                        (changedEntity.State == EntityState.Deleted)
                    {
                        entity.OnBeforeUpdate();
                        // TODO: Use await here!
                        entity.ModifiedByUserId = _userService.GetCurrentUser().Result.Id;
                        entity.IsSoftDeleted = true;
                        changedEntity.State = EntityState.Modified;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
