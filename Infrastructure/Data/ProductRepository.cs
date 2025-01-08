using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        //Create a product
        public async Task CreateProduct(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        //Delete a product
        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        //Get a product
        public async Task<Product> GetProduct(int businessId, int productId)
        {
            return await _context.Products.Where(b => b.BusinessId == businessId && b.Id == productId).FirstOrDefaultAsync();
        }

        //Get all products async
        public async Task<IReadOnlyList<Product>> GetAllProducts(int businessId)
        {
            return await _context.Products.Where(b => b.BusinessId == businessId).ToListAsync();
        }

        //Get products with page list
        public async Task<PagedList<Product>> GetProductsWithPage(int businessId, PaginationParams paginationParams)
        {
            var query = _context.Products.Where(b => b.BusinessId == businessId).OrderBy(p => p.Id).AsQueryable();
            return await PagedList<Product>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        //Update a product
        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        
    }
}
