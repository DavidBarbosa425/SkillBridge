using API.Base;
using API.Interfaces;
using API.Interfaces.Mappers;
using API.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;

        public AuthController(
            IApiMapper apiMapper,
            ILogger<BaseController> logger,
            IAuthService authService,
            ICookieService cookieService)
             : base(apiMapper, logger)
        {
            _authService = authService;
            _cookieService = cookieService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var dto = _apiMapper.User.ToRegisterUserDto(request);

            var result = await _authService.RegisterAsync(dto);

            return ReturnResult(result);
        }

        [AllowAnonymous]
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            var confirmEmailDto = _apiMapper.User.ToConfirmEmailDto(request);

            var result = await _authService.ConfirmEmailAsync(confirmEmailDto);

            return ReturnResult(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var dto = _apiMapper.User.ToLoginDto(request);

            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
                return ReturnResult(result);

            _cookieService.SetAuthCookies(result.Data.Token, result.Data.RefreshToken);

            return ReturnResult(result, auth => new
            {
                auth.User,
                auth.ExpiresIn
            });
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var dto = _apiMapper.User.ToForgotPasswordDto(request);

            var result = await _authService.ForgotPasswordAsync(dto);

            return ReturnResult(result);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var dto = _apiMapper.User.ToResetPasswordDto(request);

            var result = await _authService.ResetPasswordAsync(dto);

            return ReturnResult(result);
        }
    }
}
