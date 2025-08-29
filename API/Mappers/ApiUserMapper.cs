using API.Interfaces.Mappers;
using API.Models;
using Application.DTOs;
using Domain.Common;

namespace API.Mappers
{
    public class ApiUserMapper : IApiUserMapper
    {
        public ConfirmEmailDto ToConfirmEmailDto(ConfirmEmailRequest request)
        {
            return new ConfirmEmailDto
            {
              userId = request.UserId,
              Token = request.Token
            };
        }

        public RegisterUserDto ToRegisterUserDto(RegisterUserRequest request)
        {
            return new RegisterUserDto
            {
                Email = request.Email,
                FullName = request.FullName,
                Name = request.Name,
                Password = request.Password,
                PreferredName = request.PreferredName
            };
        }

        public LoginDto ToLoginDto(LoginRequest request)
        {
            return new LoginDto
            {
                Email = request.Email,
                Password = request.Password
            };
        }

        public ForgotPasswordDto ToForgotPasswordDto(ForgotPasswordRequest request)
        {
            return new ForgotPasswordDto
            {
                Email = request.Email
            };
        }

        public ResetPasswordDto ToResetPasswordDto(ResetPasswordRequest request)
        {
            return new ResetPasswordDto
            {
                userId = request.userId,
                Token = request.Token,
                NewPassword = request.NewPassword
            };
        }

        public Result<LoginResponse> ToLoginResponse(Result<LoginResultDto> resultDto)
        {
            var response = new LoginResponse
            {
                ExpiresIn = resultDto.Data.ExpiresIn,
                User = resultDto.Data.User
            };

            return Result<LoginResponse>.Ok(response);
        }
    }
}
