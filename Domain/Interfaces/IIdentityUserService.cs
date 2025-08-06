using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IIdentityUserService
    {
        Task<Result<User>> AddAsync(User user, string password);
        Task<Result<User>> FindByIdAsync(Guid id);
        Task<Result<User>> FindByEmailAsync(string email);
        Task<Result<User>> CheckPasswordAsync(string email,string password);
        Task<Result<string>> GenerateEmailConfirmationTokenAsync(Guid identityId);
        Task<Result> ConfirmEmailAsync(Guid userId, string token);
        Task<Result<string>> GeneratePasswordResetTokenAsync(Guid identityId);
        Task<Result> ResetPasswordAsync(string email, string token, string newPassword);
        Task<Result> AssignRoleAsync(Guid id, string role);
        Task<Result<IList<string>>> GetRolesByEmailAsync(string email);
        Task<Result<IList<string>>> GetRolesByIdAsync(Guid userId);
    }
}
