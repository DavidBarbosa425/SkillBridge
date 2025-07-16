using Application.DTOs;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result> RegisterUserAsync(RegisterUserDto dto);
        Task LoginAsync(LoginDto dto);
    }
}
