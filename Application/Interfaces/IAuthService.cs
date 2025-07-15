using Application.DTOs;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result> RegisterUserAsync(RegisterUserDto dto);
        Task<Result<SendEmail>> GenerateEmailConfirmation(UserDto userDto);
        Task<Result> ConfirmationUserEmailAsync(Guid userId, string token);
        Task LoginAsync(LoginDto dto);
    }
}
