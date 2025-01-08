using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AddOnsRepository : IAddOnsRepository
    {
        private readonly DataContext _context;

        public AddOnsRepository(DataContext context)
        {
            _context = context;
        }

        // Create an AddOn
        public async Task CreateAddOn(AddOns addOn)
        {
            await _context.AddOns.AddAsync(addOn);
            await _context.SaveChangesAsync();
        }

        // Delete an AddOn
        public async Task DeleteAddOn(AddOns addOn)
        {
            _context.AddOns.Remove(addOn);
            await _context.SaveChangesAsync();
        }

        // Get all AddOns for a product
        public async Task<IReadOnlyList<AddOns>> GetAllAddOns(int businessId, int productId)
        {
            return await _context.AddOns
                .Where(a => a.BusinessId == businessId && a.ProductId == productId)
                .ToListAsync();
        }

        // Get an AddOn
        public async Task<AddOns> GetAddOn(int businessId, int productId, int addOnId)
        {
            return await _context.AddOns
                .Where(a => a.BusinessId == businessId && a.ProductId == productId && a.Id == addOnId)
                .FirstOrDefaultAsync();
        }

        // Get AddOns with pagination
        public async Task<PagedList<AddOns>> GetAddOnsWithPage(int businessId, int productId, PaginationParams paginationParams)
        {
            var query = _context.AddOns
                .Where(a => a.BusinessId == businessId && a.ProductId == productId)
                .OrderBy(x => x.Id)
                .AsQueryable();

            return await PagedList<AddOns>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        // Update an AddOn
        public Task UpdateAddOn(AddOns addOn)
        {
            _context.AddOns.Update(addOn);
            return _context.SaveChangesAsync();
        }
    }
}
