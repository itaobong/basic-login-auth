using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginAuth.Models;

namespace LoginAuth.Controllers
{
    /// <summary>
    /// Controller handling user authentication operations including registration and login
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the AuthController
        /// </summary>
        /// <param name="userManager">The ASP.NET Core Identity user manager</param>
        /// <param name="configuration">Application configuration for accessing JWT settings</param>
        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Registers a new user in the system
        /// </summary>
        /// <param name="model">The registration details including email, password, and user information</param>
        /// <returns>Success message if registration is successful, or error details if registration fails</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User created successfully" });
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token
        /// </summary>
        /// <param name="model">The login credentials including email and password</param>
        /// <returns>JWT token if authentication is successful, or error message if authentication fails</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Invalid credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Invalid credentials");
            }

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Generates a JWT token for the authenticated user
        /// </summary>
        /// <param name="user">The authenticated user</param>
        /// <returns>JWT token string</returns>
        private string GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}