using Microsoft.EntityFrameworkCore;

namespace SquipApi.WebApi.Models
{
    public class SquipContext : DbContext
    {
        public SquipContext(DbContextOptions<SquipContext> options)
            : base(options)
        { }

        public DbSet<Squip> Squips { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Squip>().HasKey(s => s.Id);
            modelBuilder.Entity<Tag>().HasKey(t => t.Name);
            modelBuilder.Entity<SquipTag>().HasKey(k => new {k.TagName, k.SquipId});

            modelBuilder.Entity<SquipTag>().HasOne(st => st.Squip).WithMany(s => s.SquipTags)
                .HasForeignKey(st => st.SquipId);

            modelBuilder.Entity<SquipTag>().HasOne(st => st.Tag).WithMany(t => t.SquipTags)
                .HasForeignKey(st => st.TagName);
        }
    }
}
