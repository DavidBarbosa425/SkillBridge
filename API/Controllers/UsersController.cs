using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Emails;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserEmailConfirmationService _userEmailConfirmationService;

        public UsersController(
            IUserService userService,
            IUserEmailConfirmationService userEmailConfirmationService)
        {
            _userService = userService;
            _userEmailConfirmationService = userEmailConfirmationService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var result = await _userService.RegisterAsync(dto);

            return Ok(result);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] Guid userId,[FromQuery] string token)
        {
            var result = await _userEmailConfirmationService.ConfirmEmailAsync(userId, token);

            return Ok(result);
        }
    }
}
