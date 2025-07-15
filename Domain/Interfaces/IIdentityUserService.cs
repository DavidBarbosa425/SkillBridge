using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IIdentityUserService
    {
        Task<Result<User>> AddAsync(User user);
        Task<Result<User>> FindByIdAsync(string id);
        Task<Result<string>> GenerateEmailConfirmationTokenAsync(Guid userId);
        Task<Result> ConfirmationUserEmailAsync(Guid userId, string token);
        Task<Result<string>> GeneratePasswordResetTokenAsync(Guid userId);
    }
}
