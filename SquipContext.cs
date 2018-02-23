using Microsoft.EntityFrameworkCore;

namespace Squip.Models
{
    public class SquipContext : DbContext
    {
        public SquipContext(DbContextOptions<SquipContext> options) : base(options)
        {
        }

        public DbSet<SquipModel> Squips { get; set; }
    }
}
