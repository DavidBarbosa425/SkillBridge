using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(RegisterUserDto dto);
    }
}
