using Catalog.API.Database.Database;
using Catalog.API.Database.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepositories
    {
        private readonly ICatalogDbContext catalogDbContext;
        public ProductRepository(ICatalogDbContext catalogDbContext)
        {
            this.catalogDbContext = catalogDbContext;
        }


        public async Task CreateProductAsync(Product product)
        {
            await catalogDbContext.Products.InsertOneAsync(product);

        }
        public async Task<bool> DeleteProductAsync(string id)
        {
            FilterDefinition<Product> myfilter = Builders<Product>.Filter.Eq(p => p.Id, id);

            var deleteResult =  await catalogDbContext.Products.DeleteOneAsync(myfilter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName)
        {
            return await catalogDbContext.Products.Find(x => x.Name.Contains(categoryName)).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await catalogDbContext.Products.Find(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string productName)
        {
            return await catalogDbContext.Products.Find(x => x.Name.Contains(productName)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await catalogDbContext.Products.Find(x => true).ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var result = await catalogDbContext.Products.FindOneAndReplaceAsync(filter: x => x.Id == product.Id, replacement: product);

            return result != null;
        }
    }
}
