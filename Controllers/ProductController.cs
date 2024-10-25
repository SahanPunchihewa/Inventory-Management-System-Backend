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

        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
