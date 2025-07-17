using Application.DTOs;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces.Emails
{
    public interface IUserEmailConfirmationService
    {
        Task<Result> SendConfirmationEmailAsync(UserDto userDto);
        Task<Result> ConfirmEmailAsync(Guid userId, string token);
    }
}
