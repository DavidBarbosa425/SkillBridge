using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task LoginAsync(LoginDto dto);
        Task<Result> RegisterUserAsync(RegisterUserDto dto);
        Task<Result> ConfirmationUserEmailAsync(Guid userId, string token);
    }
}
