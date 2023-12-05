using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.Enums;
using Backend.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IUserService _userService;

        public UsersController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet("Find_User")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserByUserName([FromQuery] string username)
        {
            return Ok(_userService.GetUserByUsername(username));
        }

        [HttpPut("AssignRole")]
        [Authorize(Roles = "Admin")] // Restricționează accesul la acest endpoint doar pentru Admini
        public async Task<IActionResult> AssignRoleToUser([FromQuery] string username, [FromQuery] Role role)
        {
            try
            {
                await _userService.AssignRoleToUser(username, role);
                return Ok("Rolul a fost atribuit cu succes.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("RemoveRole")]
        [Authorize(Roles = "Admin")] // Restricționează accesul la acest endpoint doar pentru Admini
        public async Task<IActionResult> RemoveRoleFromUser([FromQuery] string username, [FromQuery] Role role)
        {
            try
            {
                await _userService.RemoveRoleFromUser(username, role);
                return Ok("Rolul a fost eliminat cu succes.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User
            {
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
                DateCreated = DateTime.Now
            };

            var result = await _userService.CreateUserAsync(user);

            if (result)
            {
                return Ok(new { message = "User registered successfully!" });
            }

            return BadRequest(new { message = "User registration failed." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.AuthenticateAsync(model.Username, model.Password);
            if (user != null)
            {
                // Generarea token-ului JWT
                var token = GenerateJwtToken(user);

                return Ok(new { token = token });
            }

            return Unauthorized(new { message = "Username or password is incorrect" });
        }

        [HttpDelete("Delete_User")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromQuery] string username)
        {
            var result = await _userService.DeleteUser(username);
            if (result)
            {
                return Ok("Utilizatorul a fost șters cu succes.");
            }
            return NotFound("Utilizatorul nu a fost găsit.");
        }

        [HttpPut("UpdateProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileModel model)
        {
            // Obțineți ID-ul utilizatorului autentificat din claim-urile token-ului JWT
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _userService.UpdateUserProfile(userId, model);
            if (result)
            {
                return Ok("Profilul a fost actualizat cu succes.");
            }
            return BadRequest("Actualizarea profilului a eșuat.");
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                // Adaugă alte claims după necesitate
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
