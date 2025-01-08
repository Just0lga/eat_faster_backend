using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/product/{businessId}/all")
        [HttpGet("{businessId}/all")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProducts(int businessId)
        {
            var products = await _productRepository.GetAllProducts(businessId);
            if(products == null) 
                return NotFound();
            var sortedProducts = products.OrderBy(x => x.Id).ToList();
            return Ok(sortedProducts);
        }

        // GET: api/product/{businessId}//{productId}
        [HttpGet("{businessId}/{productId}")]
        public async Task<ActionResult<Product>> GetProduct(int businessId, int productId)
        {
            var product = await _productRepository.GetProduct(businessId, productId);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        //Get products with page list
        [HttpGet("paged")]
        public async Task<ActionResult<PagedList<Product>>> GetProducts(int businessId, [FromQuery] PaginationParams paginationParams)
        {
            var pagedProducts = await _productRepository.GetProductsWithPage(businessId,paginationParams);

            Response.Headers.Append("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                pagedProducts.TotalCount,
                pagedProducts.PageSize,
                pagedProducts.CurrentPage,
                pagedProducts.TotalPages
            }));

            return Ok(pagedProducts);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product data cannot be null.");

            await _productRepository.CreateProduct(product);
            return Ok(product);
        }

        // PUT: api/product/{businessId}/{productId}
        [HttpPut("{businessId}/{productId}")]
        public async Task<ActionResult> UpdateProduct(int businessId, int productId, [FromBody] Product newProduct)
        {
            if (newProduct == null)
                return BadRequest("Product data cannot be null.");

            if(productId != newProduct.Id)
                return BadRequest();


            var oldProduct = await _productRepository.GetProduct(businessId, productId);

            if (oldProduct == null)
                return NotFound();

            oldProduct.ProductName = newProduct.ProductName;
            oldProduct.ProductDescription = newProduct.ProductDescription;
            oldProduct.ProductPrice = newProduct.ProductPrice;
            oldProduct.ProductImageURL = newProduct.ProductImageURL;
            oldProduct.ProductCategory = newProduct.ProductCategory;
            oldProduct.ProductStock = newProduct.ProductStock;

            await _productRepository.UpdateProduct(oldProduct);
            return NoContent();
        }

        // DELETE: api/product/{businessId}/{productId}
        [HttpDelete("{businessId}/{productId}")]
        public async Task<ActionResult> DeleteProduct(int businessId, int productId)
        {
            var product = await _productRepository.GetProduct(businessId, productId);
            if (product == null)
                return NotFound();

            await _productRepository.DeleteProduct(product);
            return NoContent();
        }
    }
}
