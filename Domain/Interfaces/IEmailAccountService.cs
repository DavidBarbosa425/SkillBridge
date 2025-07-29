using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces.Emails
{
    public interface IEmailAccountService
    {
        Task<Result> SendConfirmationEmailAsync(UserRegistered user);
        Task<Result> SendPasswordResetEmailAsync(UserForgotPassword user);
    }
}
