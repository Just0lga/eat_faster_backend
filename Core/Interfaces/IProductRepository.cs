using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        //Create a product
        Task CreateProduct(Product product);

        //Delete a product
        Task DeleteProduct(Product product);

        //Get a product
        Task<Product> GetProduct(int businessId, int productId);

        //Get all products async
        Task<IReadOnlyList<Product>> GetAllProducts(int businessId);

        //Get products with page list
        Task<PagedList<Product>> GetProductsWithPage(int businessId, PaginationParams paginationParams);

        //Update a product
        Task UpdateProduct(Product product);
    }
}
