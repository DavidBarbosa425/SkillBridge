using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Result> RegisterAsync(RegisterUserDto dto);
    }
}
