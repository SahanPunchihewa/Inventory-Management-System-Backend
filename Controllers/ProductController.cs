using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystemAPI.Models;
using InventoryManagementSystemAPI.Services;
using InventoryManagementSystemAPI.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagementSystemAPI.Controllers
{
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    [Authorize(Policy = IdentityData.EmployeeUserPolicyName)]
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
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [Authorize(Policy = IdentityData.EmployeeUserPolicyName)]
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            return productService.Get();
        }


        // GET: api/<ProductController>/lowStock 
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [Authorize(Policy = IdentityData.EmployeeUserPolicyName)]
        [HttpGet("lowStock")]
        public ActionResult<List<Product>> GetLowOfStockProduct()
        {
            return productService.GetLowOfStockProduct();
        }

        // GET: api/<ProductController>/outOfStock
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [Authorize(Policy = IdentityData.EmployeeUserPolicyName)]
        [HttpGet("outOfStock")]
        public ActionResult<List<Product>> GetOutOfStockProduct()
        {
            return productService.GetOutOfStockProduct();
        }

        // GET: api/<ProductController>/inventorySummary
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [Authorize(Policy = IdentityData.EmployeeUserPolicyName)]
        [HttpGet("inventorySummary")]
        public ActionResult GetInventorySummary()
        {
            var summary = productService.GetInventorySummary();
            return Ok(summary);
        }

        // Get product by ID
        // GET api/<ProductController>/5
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [Authorize(Policy = IdentityData.EmployeeUserPolicyName)]
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
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpPost("create")]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            productService.Create(product);
            return CreatedAtAction(nameof(Get), new {id = product.Id}, product);
        }

        // PUT api/<ProductController>/5
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [Authorize(Policy = IdentityData.EmployeeUserPolicyName)]
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Product product)
        {
            var updateProduct = productService.GetById(id);

            if (updateProduct == null)
            {
                return NotFound($"Product with id {id} not found");
            }

            productService.UpdateProduct(id, product);

            return Ok(product);


        }

        // DELETE api/<ProductController>/5
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
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
