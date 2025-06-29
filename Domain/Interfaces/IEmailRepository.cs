namespace Domain.Interfaces
{
    public interface IEmailRepository
    {
        Task<bool> SaveTokenEmailConfirmationAsync(string email, string token);
        Task<string> GenerateEmailConfirmationTokenAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<Guid> GetTokenEmailConfirmationIdAsync(string email);
    }
}
