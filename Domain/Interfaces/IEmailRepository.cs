using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmailRepository
    {
        Task<bool> SaveTokenEmailConfirmationAsync(EmailConfirmationToken emailConfirmationToken);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
    }
}
