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
        private readonly IUserEmailConfirmationService _emailConfirmationService;

        public AuthController(
            IAuthService authService)
        {
            _authService = authService;
            _emailConfirmationService = emailConfirmationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            await _authService.LoginAsync(dto);

            return Ok("result");
        }
    }
}
