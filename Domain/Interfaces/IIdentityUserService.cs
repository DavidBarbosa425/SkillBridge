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
        Task<Result<string>> GenerateEmailConfirmationTokenAsync(string id);
        Task<Result> ConfirmEmailAsync(string id, string token);
        Task<Result<string>> GeneratePasswordResetTokenAsync(string id);
        Task<Result> ResetPasswordAsync(string email, string token, string newPassword);
        Task<Result> AssignRoleAsync(string id, string role);
        Task<Result<IList<string>>> GetRolesByEmailAsync(string email);
        Task<Result<IList<string>>> GetRolesByIdAsync(string id);
        Task<Result<string>> GenerateRefreshTokenAsync(string id);
    }
}
