using Application.DTOs;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces.Emails
{
    public interface IEmailAccountService
    {
        Task<Result> SendConfirmationEmailAsync(UserDto userDto);
        Task<Result> SendPasswordResetEmailAsync(UserDto userDto);
    }
}
