using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces.Emails
{
    public interface IUserEmailPasswordResetService
    {
        Task<Result> SendEmailPasswordResetAsync(UserDto userDto);
    }
}
