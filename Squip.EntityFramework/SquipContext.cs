using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Squip.Pocos;

namespace Squip.EntityFramework
{
    public class SquipContext : DbContext
    {
        public SquipContext(DbContextOptions<SquipContext> options) : base(options)
        {
        }
        public DbSet<SquipPoco> Squips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=localhost;Database=squip_db;Username=postgres;password=postgres");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SquipPoco>(e =>
            {
                e.ToTable("squips");
                e.HasKey(s => s.Id);
                e.Property(s => s.Title).IsRequired();
                e.Property(s => s.Content).IsRequired();
            });
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
        {
            var changedEntities = ChangeTracker.Entries();
            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is BasePoco baseEntity)
                {
                    if (changedEntity.State == EntityState.Added)
                    {
                        baseEntity.OnBeforeInsert();
                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        baseEntity.OnBeforeUpdate();
                    }
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
