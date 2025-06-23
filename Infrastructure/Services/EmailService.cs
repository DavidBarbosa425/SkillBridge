using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Configurations;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;



namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailRepository _emailRepository;
        private readonly EmailSettings _emailSettings;
        private readonly SmtpSettings _smtpSettings;

        public EmailService(
            IHttpContextAccessor httpContextAccessor,
            IEmailRepository emailRepository,
            IOptions<EmailSettings> emailOptions,
            IOptions<SmtpSettings> smtpOptions
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _emailRepository = emailRepository;
            _emailSettings = emailOptions.Value;
            _smtpSettings = smtpOptions.Value;
        }

        public async Task SendRegistrationConfirmationAsync(string name, string email)
        {
            var applicationUser = new ApplicationUser(name, email);

            var htmlBody = await GenerateBody(applicationUser, EmailSubjects.Confirmation);
            var emailMessage = GenerateEmail(applicationUser, EmailSubjects.Confirmation, htmlBody);
            await SendEmailAsync(emailMessage);
        }
        private async Task<string> GenerateBody(ApplicationUser user, string subject)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            var scheme = request?.Scheme ?? "https";
            var host = request?.Host.ToString() ?? "localhost";

            var htmlBody = string.Empty;

            if (subject == EmailSubjects.Confirmation)
            {
                var token = await _emailRepository.GenerateEmailConfirmationTokenAsync(user);
                var emailConfirmationToken = new EmailConfirmationToken(user, token);
                var result = await _emailRepository.SaveTokenEmailConfirmationAsync(emailConfirmationToken);

                var confirmationLink = $"{scheme}://{host}/api/auth/confirmationUserEmail?id={emailConfirmationToken.Id}";

                htmlBody = $@"
                <p>Olá {emailConfirmationToken.Name},</p>
                <p>Clique no botão abaixo para confirmar seu e-mail:</p>
                <p><a style='padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none;' href='{confirmationLink}'>Confirmar E-mail</a></p>";

                return htmlBody;
            }


            return htmlBody;

        }

        private MimeMessage GenerateEmail(ApplicationUser user, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.Sender, _emailSettings.From));
            emailMessage.To.Add(new MailboxAddress("", user.Email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = body };

            return emailMessage;
        }

        private async Task SendEmailAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
       
    }
}
