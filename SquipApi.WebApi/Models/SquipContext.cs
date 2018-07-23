using Microsoft.EntityFrameworkCore;

namespace SquipApi.WebApi.Models
{
    public class SquipContext : DbContext
    {
        private readonly ITenantProvider _tenantProvider;

        public SquipContext(DbContextOptions<SquipContext> options, ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        public DbSet<Squip> Squips { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Tag> SquipTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Squip>().HasQueryFilter(s => s.TenantId == _tenantProvider.TenantId);
            modelBuilder.Entity<Squip>(b =>
            {
                b.ToTable("Squip");
                b.HasKey(s => s.Id);
                b.Property(s => s.Title).IsRequired();
                b.Property(s => s.Body).IsRequired();
            });

            modelBuilder.Entity<Tag>(b =>
            {
                b.ToTable("Tag");
                b.HasKey(t => t.Id);
                b.HasAlternateKey(t => t.Name);
            });

            modelBuilder.Entity<SquipTag>(b =>
            {
                b.ToTable("SquipTag");
                b.HasKey(k => new { k.TagId, k.SquipId });
                b.HasOne(st => st.Squip).WithMany(s => s.SquipTags)
                    .HasForeignKey(st => st.SquipId);
                b.HasOne(st => st.Tag).WithMany(t => t.SquipTags)
                    .HasForeignKey(st => st.TagId);
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
                        entity.TenantId = _tenantProvider.TenantId;
                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.OnBeforeUpdate();
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
