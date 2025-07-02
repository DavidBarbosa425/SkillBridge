using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Configurations;
using Infrastructure.Repositories;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;



namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly SmtpSettings _smtpSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmailService(
            IOptions<EmailSettings> emailOptions,
            IOptions<SmtpSettings> smtpOptions,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _emailSettings = emailOptions.Value;
            _smtpSettings = smtpOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.Sender, _emailSettings.From));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

        }
        public string GenerateLinkEndPoint(string controller, string method, string? Idparam)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            var scheme = request?.Scheme ?? "https";
            var host = request?.Host.ToString() ?? "localhost";

            var endPoint = $"{scheme}://{host}/api/{controller}/{method}";

            endPoint = string.IsNullOrEmpty(Idparam) ? endPoint : $"{endPoint}?id={Idparam}";

            return endPoint;
        }


    }
}
