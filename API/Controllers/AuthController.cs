using API.Extensions;
using API.Interfaces;
using API.Interfaces.Mappers;
using API.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var dto = _apiMapper.User.ToRegisterUserDto(request);

            var result = await _authService.RegisterAsync(dto);

            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            var confirmEmailDto = _apiMapper.User.ToConfirmEmailDto(request);

            var result = await _authService.ConfirmEmailAsync(confirmEmailDto);

            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var dto = _apiMapper.User.ToLoginDto(request);

            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
                return this.ToActionResult(result);

            _cookieService.SetAuthCookies(result.Data.Token, result.Data.RefreshToken);

            var response = _apiMapper.User.ToLoginResponse(result);

            return this.ToActionResult(response);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var dto = _apiMapper.User.ToForgotPasswordDto(request);

            var result = await _authService.ForgotPasswordAsync(dto);

            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var dto = _apiMapper.User.ToResetPasswordDto(request);

            var result = await _authService.ResetPasswordAsync(dto);

            return this.ToActionResult(result);
        }
    }
}
