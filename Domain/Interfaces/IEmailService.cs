using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(SendEmail sendEmail);

        Task<Result> ConfirmationUserEmailAsync(User user, string token);

        Task<Result<string>> GenerateEmailConfirmationTokenAsync(User user);

        Task<Result<string>> GeneratePasswordResetTokenAsync(User user);
    }
}
