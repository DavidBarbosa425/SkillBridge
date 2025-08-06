using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}
