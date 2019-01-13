using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Squip.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Squip.Api.Repositories
{
    public class SquipRepository : ISquipRepository
    {
        private readonly SquipContext _context;

        public SquipRepository(SquipContext context)
        {
            _context = context;
        }

        public async Task<SquipDto> DeleteSquipAsync(SquipDto squip)
        {
            _context.Squips.Remove(squip);
            await _context.SaveChangesAsync();

            return squip;
        }

        public async Task<SquipDto> CreateSquipAsync(SquipDto squip)
        {
            _context.Squips.Add(squip);
            await _context.SaveChangesAsync();

            return squip;
        }

        public async Task<IEnumerable<SquipDto>> GetMostRecentSquipsAsync()
        {
            var squips = await _context.Squips.OrderByDescending(s => s.CreatedAt).ToListAsync();

            return squips;
        }

        public async Task<SquipDto> GetSquipByIdAsync(long id)
        {
            var squip = await _context.Squips.FindAsync(id);

            return squip;
        }

        public async Task<SquipDto> UpdateSquipAsync(SquipDto squip)
        {
            _context.Entry(squip).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return squip;
        }
    }
}
