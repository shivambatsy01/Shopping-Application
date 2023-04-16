using Catalog.API.Database.Database;
using Catalog.API.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public HomeController(IConfiguration configuration) //configuration object created using dependency
        {
            this.configuration = configuration;
        }

        [HttpGet]
        [Route("index")]
        public async Task<IActionResult> Index()
        {

            var client = new MongoClient(configuration.GetValue<string>("ConnectionStrings:MongoConnection"));
            var database = client.GetDatabase(configuration.GetValue<string>("ConnectionStrings:DatabaseName"));
            var collection = database.GetCollection<Product>(configuration.GetValue<string>("ConnectionStrings:CollectionName"));
            var items = await collection.Find(x => true).ToListAsync();
            return Ok();
            
        }
    }
}
