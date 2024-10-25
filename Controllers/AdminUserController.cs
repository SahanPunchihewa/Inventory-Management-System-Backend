using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using InventoryManagementSystemAPI.Identity;
using InventoryManagementSystemAPI.Services;
using InventoryManagementSystemAPI.Models;
using System.Security.Claims;
using System.Text;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagementSystemAPI.Controllers
{
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IConfiguration _configuration;

        public AdminUserController(IAdminService adminService, IConfiguration configuration)
        {
            this.adminService = adminService;
            this._configuration = configuration;
        }

        // Generate JWT Token
        private string CreateToken(AdminUser adminUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, adminUser.Id)
            };

            // Add Admin User role to claims
            claims.Add(new Claim(IdentityData.AdminUserClaimName, "true"));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("JwtSettings:Key").Value!));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                             _configuration.GetSection("JwtSettings:Issuer").Value!,
                             _configuration.GetSection("JwtSettings:Audience").Value!,
                             claims: claims,
                             expires: DateTime.Now.AddDays(1),
                             signingCredentials: creds
          );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        // Admin User create an account
        // POST api/<AdminUserController>
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<AdminUser> Register([FromBody] AdminUser request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            request.Password = passwordHash;

            adminService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = request.Id }, request);

        }

        // POST api/<AdminUserControleer>/login
        [AllowAnonymous]
        [HttpPost("login")]

        public ActionResult<AdminUser> Login([FromBody] AdminUser request)
        {
            var adminUser = adminService.GetByUsername(request.Username);

            if(adminUser == null)
            {
                return BadRequest("Invalid Credentials");
            }

            bool verified = BCrypt.Net.BCrypt.Verify(request.Password, adminUser.Password);

            if(!verified)
            {
                return BadRequest("Invalid Credentials");
            }
            string jwt = CreateToken(adminUser);

            return Ok(new
            {
                Id = adminUser.Id,
                UserName = adminUser.Username,
                Token = jwt,
                Role = IdentityData.AdminUserClaimName
            });
        }

        // GET: api/<AdminUserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AdminUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }



        // PUT api/<AdminUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
