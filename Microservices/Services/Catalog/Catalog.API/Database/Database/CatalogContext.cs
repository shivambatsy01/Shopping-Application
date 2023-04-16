using Catalog.API.Database.Entities;
using MongoDB.Driver;

namespace Catalog.API.Database.Database
{
    public class CatalogContext : ICatalogDbContext
    {
        public IMongoClient mongoClient;
        public IMongoCollection<Product> Products { get; }
        public IMongoDatabase mongoDatabase { get; }


        public CatalogContext(IConfiguration configuration)  //dependency injection for configuration
        {
            var mongoConnectionString = configuration.GetConnectionString("MongoConnection");
            var mongoDataBaseName = configuration.GetConnectionString("DatabaseName");
            var collectionName = configuration.GetConnectionString("CollectionName");

            mongoClient = new MongoClient(mongoConnectionString);
            mongoDatabase = mongoClient.GetDatabase(mongoDataBaseName);
            Products = mongoDatabase.GetCollection<Product>(collectionName);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            //var products = Products.Find(null);
            //var products = await mongoDatabase.GetCollection<Product>("Products").Find(x => true).ToListAsync();
            var products = await Products.FindAsync(x => true);
            //return products;
            return new List<Product>();
        }


        public async void SeedData()
        {
            bool exist = Products.Find(x => true).Any();
            await Products.InsertManyAsync(seedData);
        }


        List<Product> seedData = new List<Product>
        {
            new Product
            {
                Id = "6413c61b642c0e2e46168705",
                Name = "HP Laptop1",
                Category = "Laptops",
                Summary = "data seed manually from c# context class",
                Description = "Description",
                ImageFile = "image file",
                Price = (decimal)12.14
            },
            new Product
            {
                Id = "6413c61b642c0e2e38168705",
                Name = "HP Laptop2",
                Category = "Laptops",
                Summary = "data seed manually from c# context class",
                Description = "Description",
                ImageFile = "image file",
                Price = (decimal)14.14
            },
            new Product
            {
                Id = "6413c61b642c0e2e461687ab",
                Name = "HP Laptop3",
                Category = "Laptops",
                Summary = "data seed manually from c# context class",
                Description = "Description",
                ImageFile = "image file",
                Price = (decimal)16.14
            },
        };

    }
}
