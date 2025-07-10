using Domain.Common;

namespace Domain.Interfaces
{
    public interface IEmailRepository
    {
        Task<Result<string>> GenerateEmailConfirmationTokenAsync(string email);
        Task<Result> SaveTokenEmailConfirmationAsync(string email, string token);
        Task<Result<Guid>> GetEmailConfirmationTokenGuidAsync(string email);
        Task<Result<string>> GeneratePasswordResetTokenAsync(string email);
    }
}
