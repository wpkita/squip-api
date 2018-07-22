using Microsoft.EntityFrameworkCore;
using SquipApi.Models;

namespace SquipApi.Web.Models
{
    public class SquipContext : DbContext
    {
        public SquipContext(DbContextOptions<SquipContext> options)
            : base(options)
        { }

        public DbSet<Squip> Squips { get; set; }
    }
}
