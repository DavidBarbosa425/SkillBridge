using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IIdentityUserService
    {
        Task<Result<User>> AddAsync(User user);
        Task<Result<User>> FindByIdAsync(string id);
        Task<Result<User>> FindByEmailAsync(string email);
        Task<Result<User>> CheckPasswordAsync(string Email,string Password);
        Task<Result<string>> GenerateEmailConfirmationTokenAsync(Guid userId);
        Task<Result> ConfirmEmailAsync(Guid userId, string token);
        Task<Result<string>> GeneratePasswordResetTokenAsync(Guid userId);
    }
}
