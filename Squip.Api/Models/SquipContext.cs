using Microsoft.EntityFrameworkCore;

namespace Squip.Api.Models
{
    public class SquipContext : DbContext
    {
        public SquipContext(DbContextOptions<SquipContext> options) : base(options)
        {

        }
        public DbSet<SquipDto> Squips { get; set; }
    }
}
