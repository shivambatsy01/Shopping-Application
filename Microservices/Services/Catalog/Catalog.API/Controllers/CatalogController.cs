using Catalog.API.Database.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepositories productRepositories;
        private readonly ILogger<CatalogController> logger;
        public CatalogController(IProductRepositories productRepositories, ILogger<CatalogController> logger)
        {
            this.productRepositories = productRepositories;
            this.logger = logger;
        }




        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await productRepositories.GetProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCodes.Status500InternalServerError + ex.Message);
            }
        }

        [HttpGet]
        [Route("products/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            try
            {
                var product = await productRepositories.GetProductByIdAsync(id);
                if(product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCodes.Status500InternalServerError + ex.Message);
            }
        }

        [HttpDelete]
        [Route("products/{id}")]
        public async Task<IActionResult> DeleteProductsById(string id)
        {
            try
            {
                var result = await productRepositories.DeleteProductAsync(id);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(StatusCodes.Status500InternalServerError + ex.Message);
            }
        }

        [HttpGet]
        [Route("products/name/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            try
            {
                var products = await productRepositories.GetProductsByNameAsync(name);
                return Ok(products);

            }
            catch (Exception ex)
            {
                return BadRequest(StatusCodes.Status500InternalServerError + ex.Message);
            }
        }

        [HttpGet]
        [Route("products/category/{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            try
            {
                var products = await productRepositories.GetProductsByCategoryAsync(category);
                return Ok(products);

            }
            catch (Exception ex)
            {
                return BadRequest(StatusCodes.Status500InternalServerError + ex.Message);
            }
        }

        [HttpPost]
        [Route("products/add")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product) //use here requestDTO and responseDTO and mappers and fluent validations
        {
            try
            {
                await productRepositories.CreateProductAsync(product);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(StatusCodes.Status500InternalServerError + ex.Message);
            }
        }

        [HttpPut]
        [Route("products/update")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            try
            {
                var result = await productRepositories.UpdateProductAsync(product);
                if (result == true)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(StatusCodes.Status500InternalServerError + ex.Message);
            }
        }
    }
}
