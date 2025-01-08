using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AddOnsOptionRepository : IAddOnsOptionRepository
    {
        private readonly DataContext _context;

        public AddOnsOptionRepository(DataContext context)
        {
            _context = context;
        }

        // Create an AddOnsOption
        public async Task CreateAddOnsOption(AddOnsOption addOnsOption)
        {
            await _context.AddOnsOptions.AddAsync(addOnsOption);
            await _context.SaveChangesAsync();
        }

        // Delete an AddOnsOption
        public async Task DeleteAddOnsOption(AddOnsOption addOnsOption)
        {
            _context.AddOnsOptions.Remove(addOnsOption);
            await _context.SaveChangesAsync();
        }

        // Get all AddOnsOptions for an AddOns
        public async Task<IReadOnlyList<AddOnsOption>> GetAllAddOnsOptions(int businessId, int productId, int addOnsId)
        {
            return await _context.AddOnsOptions
                .Where(o => o.BusinessId == businessId && o.ProductId == productId && o.AddOnsId == addOnsId)
                .ToListAsync();
        }

        // Get an AddOnsOption
        public async Task<AddOnsOption> GetAddOnsOption(int businessId, int productId, int addOnsId, int optionId)
        {
            return await _context.AddOnsOptions
                .Where(o => o.BusinessId == businessId && o.ProductId == productId && o.AddOnsId == addOnsId && o.Id == optionId)
                .FirstOrDefaultAsync();
        }

        // Get AddOnsOptions with pagination
        public async Task<PagedList<AddOnsOption>> GetAddOnsOptionsWithPage(int businessId, int productId, int addOnsId, PaginationParams paginationParams)
        {
            var query = _context.AddOnsOptions
                .Where(o => o.BusinessId == businessId && o.ProductId == productId && o.AddOnsId == addOnsId)
                .OrderBy(x => x.Id)
                .AsQueryable();

            return await PagedList<AddOnsOption>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        // Update an AddOnsOption
        public Task UpdateAddOnsOption(AddOnsOption addOnsOption)
        {
            _context.AddOnsOptions.Update(addOnsOption);
            return _context.SaveChangesAsync();
        }
    }
}
