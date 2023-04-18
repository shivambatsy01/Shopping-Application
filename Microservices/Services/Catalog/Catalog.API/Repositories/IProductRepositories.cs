using Catalog.API.Database.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepositories
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<IEnumerable<Product>> GetProductsByNameAsync(string productName);

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName);

        Task<Product> GetProductByIdAsync(string id);

        Task CreateProductAsync(Product product);

        Task<bool> UpdateProductAsync(Product product);

        Task<bool> DeleteProductAsync(string id);
    }
}
