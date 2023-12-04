using Backend.Models.Enums;
using Backend.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUserByUserName([FromQuery] string username)
        {
            return Ok(_userService.GetUserByUsername(username));
        }

        [HttpPost("AssignRole")]
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

        [HttpPost("RemoveRole")]
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
    }
}
