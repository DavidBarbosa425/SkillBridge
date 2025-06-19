using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        string RegisterUserAsync(RegisterUserDto dto);
    }
}
