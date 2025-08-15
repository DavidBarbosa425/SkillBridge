using API.Interfaces;
using API.Interfaces.Mappers;
using API.Models;
using Application.DTOs;
using Application.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IApiMapper _apiMapper;
        private readonly ICookieService _cookieService;

        public AuthController(
            IAuthService authService,
            IApiMapper apiMapper,
            ICookieService cookieService)
        {
            _authService = authService;
            _apiMapper = apiMapper;
            _cookieService = cookieService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            return Ok(result);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            var confirmEmailDto = _apiMapper.User.ToConfirmEmailDto(request);

            var result = await _authService.ConfirmEmailAsync(confirmEmailDto);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);


            if (!result.Success)
            {
                return Ok(new
                {
                    result.Success,
                    result.Message
                });
            }

            _cookieService.SetAuthCookies(result.Data.Token, result.Data.RefreshToken);

            return Ok(new
            {
                result.Success,
                result.Data.User,
                result.Message,
                result.Data.ExpiresIn
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var result = await _authService.ForgotPasswordAsync(dto);

            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var result = await _authService.ResetPasswordAsync(dto);

            return Ok(result);
        }
    }
}
