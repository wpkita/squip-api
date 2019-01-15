using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Squip.Pocos;

namespace Squip.EntityFramework.Repositories
{
    public class EntityFrameworkSquipRepository : ISquipRepository
    {
        private readonly SquipContext _context;

        public EntityFrameworkSquipRepository(SquipContext context)
        {
            _context = context;
        }

        public async Task<SquipPoco> DeleteSquipAsync(SquipPoco squip)
        {
            _context.Squips.Remove(squip);
            await _context.SaveChangesAsync();

            return squip;
        }

        public async Task<SquipPoco> CreateSquipAsync(SquipPoco squip)
        {
            _context.Squips.Add(squip);
            await _context.SaveChangesAsync();

            return squip;
        }

        public async Task<IEnumerable<SquipPoco>> GetMostRecentSquipsAsync()
        {
            var squips = await _context.Squips.OrderByDescending(s => s.CreatedAt).ToListAsync();

            return squips;
        }

        public async Task<SquipPoco> GetSquipByIdAsync(long id)
        {
            var squip = await _context.Squips.FindAsync(id);

            return squip;
        }

        public async Task<SquipPoco> UpdateSquipAsync(SquipPoco squip)
        {
            _context.Entry(squip).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return squip;
        }
    }
}
