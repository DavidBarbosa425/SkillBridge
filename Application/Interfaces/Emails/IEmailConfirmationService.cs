using Application.DTOs;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces.Emails
{
    public interface IEmailConfirmationService
    {
        Task<Result> SendEmailConfirmation(UserDto userDto);
        Task<Result> ConfirmationUserEmailAsync(Guid userId, string token);
    }
}
