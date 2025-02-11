using MamunTutorial.DTOs;
using MamunTutorial.Models;
using MamunTutorial.Services;
using Microsoft.AspNetCore.Mvc;

namespace MamunTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequestDTO LoginRequestDTO)
        {
            var user = await _authService.ValidateUserAsync(LoginRequestDTO.Identifier, LoginRequestDTO.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok($"Welcome, {LoginRequestDTO.Identifier}! Role: {user.Role}");
        }
    }
}
