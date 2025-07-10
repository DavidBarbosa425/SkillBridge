using Application.DTOs;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEmailConfirmationService
    {
        Task<Result<SendEmail>> GenerateEmailConfirmation(UserDto userDto);
    }
}
