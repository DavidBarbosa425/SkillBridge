using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmailRepository
    {
        Task<Result<string>> GenerateEmailConfirmationTokenAsync(string email);
        Task<Result> SaveTokenEmailConfirmationAsync(string email, string token);
        Task<Result<Guid>> GetEmailConfirmationTokenGuidAsync(string email);
        Task<Result<string>> GeneratePasswordResetTokenAsync(string email);
        Task<Result<EmailConfirmationToken>> GetEmailConfirmationTokenAsync(Guid id);
        Task<Result> ConfirmationUserEmailAsync(User user, EmailConfirmationToken emailConfirmationToken);
        Task<Result> RemoveTokenConfirmationUserEmailAsync(EmailConfirmationToken emailConfirmationToken);
    }
}
