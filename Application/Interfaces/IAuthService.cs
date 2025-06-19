using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        string RegisterAsync(RegisterDto dto);
    }
}
