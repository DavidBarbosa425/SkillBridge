using API.Models;
using Application.DTOs;
using Domain.Common;

namespace API.Interfaces.Mappers
{
    public interface IApiUserMapper
    {
        ConfirmEmailDto ToConfirmEmailDto(ConfirmEmailRequest confirmEmailRequest);
        RegisterUserDto ToRegisterUserDto(RegisterUserRequest request);
        LoginDto ToLoginDto(LoginRequest request);
        ForgotPasswordDto ToForgotPasswordDto(ForgotPasswordRequest request);
        ResetPasswordDto ToResetPasswordDto(ResetPasswordRequest request);
        Result<LoginResponse> ToLoginResponse(Result<LoginResultDto> dto);

    }
}
