using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.Enums;
using Backend.Services.UserService;
using Backend.Services.Token_JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Data;

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
                return Ok(new { message = "Rolul a fost atribuit cu succes." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("RemoveRole")]
        [Authorize(Roles = "Admin")] // Restricționează accesul la acest endpoint doar pentru Admini
        public async Task<IActionResult> RemoveRoleFromUser([FromQuery] string username)
        {
            try
            {
                await _userService.RemoveRoleFromUser(username);
                return Ok(new { message = "Rolul a fost eliminat cu succes." });
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
                return Ok(new { message = "Utilizatorul a fost șters cu succes." });
            }
            return NotFound(new { message = "Utilizatorul nu a fost găsit." });
        }

        [HttpPut("UpdateProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileModel model)
        {
            // Obținem ID-ul utilizatorului autentificat din claim-urile token-ului JWT
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            var result = await _userService.UpdateUserProfile(userId, model);
            if (result)
            {
                return Ok(new { message = " Profilul a fost actualizat cu succes." });
            }
            return BadRequest("Actualizarea profilului a eșuat.");
        }

        [HttpGet("Account")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            // Obținem ID-ul utilizatorului autentificat din claim-urile token-ului JWT
            var jti = User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;
            Guid id = Guid.Parse(jti);
            var user = await _userService.GetById(id);
            
            var userDetails = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password.Length
            };

            return Ok(userDetails);
        }

        [HttpGet("Validare")]
        [Authorize]
        public async Task<IActionResult> Val()
        {
            // Obținem ID-ul utilizatorului autentificat din claim-urile token-ului JWT
            var jti = User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;
            Guid id = Guid.Parse(jti);
            var user = await _userService.GetById(id);

            return Ok(user.Role);
        }
    }
}
