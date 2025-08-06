using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IIdentityUserService
    {
        Task<Result<User>> AddAsync(User user, string password);
        Task<Result<User>> FindByIdAsync(string id);
        Task<Result<User>> FindByEmailAsync(string email);
        Task<Result<User>> CheckPasswordAsync(string email,string password);
        Task<Result<string>> GenerateEmailConfirmationTokenAsync(string userId);
        Task<Result> ConfirmEmailAsync(Guid userId, string token);
        Task<Result<string>> GeneratePasswordResetTokenAsync(string userId);
        Task<Result> ResetPasswordAsync(string email, string token, string newPassword);
        Task<Result> AssignRoleAsync(Guid id, string role);
        Task<Result<IList<string>>> GetRolesByEmailAsync(string email);
        Task<Result<IList<string>>> GetRolesByIdAsync(Guid userId);
    }
}
