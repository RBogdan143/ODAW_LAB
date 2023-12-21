using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.Enums;
using Backend.Services.UserService;
using Backend.Services.Token_JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtTokenService _JwtToken;

        public UsersController(IUserService userService, JwtTokenService JwtToken)
        {
            _userService = userService;
            _JwtToken = JwtToken;
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.AuthenticateAsync(model.Username, model.Password);
            if (user != null)
            {
                // Generarea token-ului JWT
                var token = _JwtToken.GenerateToken(user);

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
            // Obținem ID-ul utilizatorului autentificat din claim-urile token-ului JWT
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _userService.UpdateUserProfile(userId, model);
            if (result)
            {
                return Ok("Profilul a fost actualizat cu succes.");
            }
            return BadRequest("Actualizarea profilului a eșuat.");
        }
    }
}
