using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEmailConfirmationService
    {
        Task<SendEmail> GenerateEmailConfirmation(UserDto user);
    }
}
