using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystemAPI.Models;
using InventoryManagementSystemAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        // Get all products
        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            return productService.Get();
        }

        // Get product by ID
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(string id)
        {
            var product = productService.GetById(id);

            if (product == null)
            {
                return NotFound($"Product with id {id} not found");
            }

            return product;
        }

        // Create Product Controller
        // POST api/<ProductController>
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            productService.Create(product);
            return CreatedAtAction(nameof(Get), new {id = product.Id}, product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Product product)
        {
            var updateProduct = productService.GetById(id);

            if (updateProduct == null)
            {
                return NotFound($"Product with id {id} not found");
            }

            productService.UpdateProduct(id, product);

            return Ok(updateProduct);


        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var deleteProduct = productService.GetById(id);

            if(deleteProduct == null)
            {
                return NotFound($"Product with id {id} not found");
            }

            productService.DeleteProduct(id);

            return Ok($"Product with id {id} deleted");
        }
    }
}
