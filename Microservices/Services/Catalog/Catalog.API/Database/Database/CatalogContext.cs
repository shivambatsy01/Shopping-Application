using Catalog.API.Database.Entities;
using MongoDB.Driver;

namespace Catalog.API.Database.Database
{
    public class CatalogContext : ICatalogDbContext
    {
        private IMongoClient mongoClient;
        private IMongoDatabase mongoDatabase;
        public IMongoCollection<Product> Products { get; }


        public CatalogContext(IConfiguration configuration)  //dependency injection for configuration
        {
            var mongoConnectionString = configuration.GetConnectionString("MongoConnection");
            var mongoDataBaseName = configuration.GetConnectionString("DatabaseName");
            var collectionName = configuration.GetConnectionString("CollectionName");

            mongoClient = new MongoClient(mongoConnectionString);
            mongoDatabase = mongoClient.GetDatabase(mongoDataBaseName);
            Products = mongoDatabase.GetCollection<Product>(collectionName);
        }







        //----------------------------------------------------------------------------------------------------
        //-------------------------------used for initial data seeding----------------------------------------

        public async void SeedData()  //used for initial data seed
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
