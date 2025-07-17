using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task LoginAsync(LoginDto dto);
    }
}
