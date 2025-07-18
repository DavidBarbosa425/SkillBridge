using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterUserDto dto);
        Task<Result> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);
        Task<Result<UserDto>> LoginAsync(LoginDto dto);
        Task<Result> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<Result> ResetPasswordAsync(ResetPasswordDto dto);
    }
}
