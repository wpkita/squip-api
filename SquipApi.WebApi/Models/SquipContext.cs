using Microsoft.EntityFrameworkCore;

namespace SquipApi.WebApi.Models
{
    public class SquipContext : DbContext
    {
        public SquipContext(DbContextOptions<SquipContext> options)
            : base(options)
        { }

        public DbSet<Squip> Squips { get; set; }
    }
}
