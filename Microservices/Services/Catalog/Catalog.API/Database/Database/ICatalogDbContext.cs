using Catalog.API.Database.Entities;
using MongoDB.Driver;

namespace Catalog.API.Database.Database
{
    public interface ICatalogDbContext
    {
        IMongoCollection<Product> Products {get;}
    }
}
