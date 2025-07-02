using Domain.Common;

namespace Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
        string GenerateLinkEndPoint(string controller, string method, string? Idparam);
    }
}
