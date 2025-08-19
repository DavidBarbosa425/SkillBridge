using API.Models;
using Application.DTOs;

namespace API.Interfaces.Mappers
{
    public interface IApiUserMapper
    {
        ConfirmEmailDto ToConfirmEmailDto(ConfirmEmailRequest confirmEmailRequest);
        RegisterUserDto ToRegisterUserDto(RegisterUserRequest request);
        LoginDto ToLoginDto(LoginRequest request);
        ForgotPasswordDto ToForgotPasswordDto(ForgotPasswordRequest request);
        ResetPasswordDto ToResetPasswordDto(ResetPasswordRequest request);
    }
}
