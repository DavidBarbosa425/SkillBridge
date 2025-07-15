using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {

            var result = await _authService.RegisterUserAsync(dto);

            return Ok(result);
        }

        [HttpGet("confirmationUserEmail")]
        public async Task<IActionResult> ConfirmationUserEmail(Guid userId, string token)
        {
            var result = await _authService.ConfirmationUserEmailAsync(userId, token);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            await _authService.LoginAsync(dto);

            return Ok("result");
        }
    }
}
