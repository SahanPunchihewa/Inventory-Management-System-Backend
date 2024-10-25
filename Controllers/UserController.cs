using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystemAPI.Identity;
using InventoryManagementSystemAPI.Models;
using InventoryManagementSystemAPI.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagementSystemAPI.Controllers
{
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    [Authorize(Policy = IdentityData.EmployeeUserPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this._configuration = configuration;
        }

        private string CreateToken(User user, string role)
        {
            // ID and Role add to claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(ClaimTypes.Role, role)

            };

            // Add User role to claims
            claims.Add(new Claim(IdentityData.AdminUserClaimName, "true"));
            claims.Add(new Claim(IdentityData.EmployeeUserClaimName, "true"));

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

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>/register
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<User> Register([FromBody] User request)
        {

            var checkUsername = userService.GetByUsername(request.Username);
            if (checkUsername != null)
            {

                return BadRequest("Username is already taken");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            request.Password = passwordHash;

            userService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = request.Id }, request);
        }

        // POST api/<UserController>/login
        [AllowAnonymous]
        [HttpPost("login")]

        public ActionResult<User> Login([FromBody] User request)
        {
            var user = userService.GetByUsername(request.Username);

            if (user == null)
            {
                return BadRequest("Invalid Credentials");

            }

            bool verified = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (!verified)
            {
                return BadRequest("Invalid Credentials");

            }
            string role = user.Role == "ADMIN" ? IdentityData.AdminUserClaimName : IdentityData.EmployeeUserClaimName;

            string jwt = CreateToken(user, role);

            return Ok(new
            {
                Id = user.Id,
                UserName = user.Username,
                Token = jwt,
                Role = role
            });
        }


        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
