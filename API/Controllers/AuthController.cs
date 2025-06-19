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
        public IActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            var result = _authService.RegisterUserAsync(dto);

            return Ok(result);
        }
    }
}
