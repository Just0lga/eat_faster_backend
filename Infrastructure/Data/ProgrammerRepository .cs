using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProgrammerRepository : IProgrammerRepository
    {
        private readonly DataContext _context;

        public ProgrammerRepository(DataContext context)
        {
            _context = context;
        }

        // Create a programmer
        public async Task CreateProgrammer(Programmer programmer)
        {
            await _context.Programmers.AddAsync(programmer);
            await _context.SaveChangesAsync();
        }

        // Delete a programmer
        public async Task DeleteProgrammer(Programmer programmer)
        {
            _context.Programmers.Remove(programmer);
            await _context.SaveChangesAsync();
        }

        // Get all programmers async
        public async Task<IReadOnlyList<Programmer>> GetAllProgrammers(int businessId)
        {
            return await _context.Programmers.Where(p => p.BusinessId == businessId).ToListAsync();
        }

        // Get a programmer async
        public async Task<Programmer> GetProgrammer(int businessId, int programmerId)
        {
            return await _context.Programmers
                .Where(p => p.BusinessId == businessId && p.Id == programmerId)
                .FirstOrDefaultAsync();
        }

        // Get programmers with pagination
        public async Task<PagedList<Programmer>> GetProgrammersWithPage(int businessId, PaginationParams paginationParams)
        {
            var query = _context.Programmers.Where(p => p.BusinessId == businessId).OrderBy(x => x.Id).OrderBy(x => x.Id).AsQueryable();
            return await PagedList<Programmer>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        // Update a programmer
        public Task UpdateProgrammer(Programmer programmer)
        {
            _context.Programmers.Update(programmer);
            return _context.SaveChangesAsync();
        }
    }
}
