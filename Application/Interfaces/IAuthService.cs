using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> RegisterUserAsync(RegisterUserDto dto);
    }
}
