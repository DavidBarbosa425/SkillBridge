using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmailRepository
    {
        Task<Result<EmailConfirmationToken>> SaveTokenEmailConfirmationAsync(User user, string token);
        Task<Result<EmailConfirmationToken>> GetEmailConfirmationTokenAsync(Guid id);
        Task<Result> RemoveTokenConfirmationUserEmailAsync(EmailConfirmationToken emailConfirmationToken);
    }
}
