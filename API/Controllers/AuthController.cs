using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Emails;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailConfirmationService _emailConfirmationService;

        public AuthController(
            IAuthService authService,
            IEmailConfirmationService emailConfirmationService)
        {
            _authService = authService;
            _emailConfirmationService = emailConfirmationService;
        }
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            var result = await _authService.RegisterUserAsync(dto);

            return Ok(result);
        }

        [HttpGet("confirmation-user-email")]
        public async Task<IActionResult> ConfirmationUserEmail(Guid userId, string token)
        {
            var result = await _emailConfirmationService.ConfirmationUserEmailAsync(userId, token);

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
