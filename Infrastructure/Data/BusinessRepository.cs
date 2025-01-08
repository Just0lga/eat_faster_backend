using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly DataContext _context;

        public BusinessRepository(DataContext context)
        {
            _context = context;
        }

        //Create a business
        public async Task CreateBusiness(Business business)
        {
            await _context.AddAsync(business);
            await _context.SaveChangesAsync();
        }

        //Delete a business
        public async Task DeleteBusiness(Business business)
        {
            _context.Remove(business);
            await _context.SaveChangesAsync();
        }

        //Get all businesses async
        public async Task<IReadOnlyList<Business>> GetAllBusinesses()
        {
            return await _context.Businesses.ToListAsync();
        }

        //Get a business async
        public async Task<Business> GetBusiness(int id)
        {
            return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == id);
        }

        //Get businesses with page list
        public async Task<PagedList<Business>> GetBusinessesWithPage(PaginationParams paginationParams)
        {
            var query = _context.Businesses.AsQueryable();
            return await PagedList<Business>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        //Update a business
        public async Task UpdateBusiness(Business business)
        {
            _context.Businesses.Update(business);
            await _context.SaveChangesAsync();
        }
    }

}
