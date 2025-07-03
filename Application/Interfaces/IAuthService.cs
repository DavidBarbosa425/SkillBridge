using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task LoginAsync(LoginDto dto);
        Task<Result<string>> RegisterUserAsync(RegisterUserDto dto);
    }
}
