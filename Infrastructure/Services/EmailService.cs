using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Configurations;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;


namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly SmtpSettings _smtpSettings;

        public EmailService(
            IOptions<EmailSettings> emailOptions,
            IOptions<SmtpSettings> smtpOptions
            )
        {
            _emailSettings = emailOptions.Value;
            _smtpSettings = smtpOptions.Value;

        }

        public async Task SendEmailAsync(SendEmail sendEmail)
        {

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.Sender, _emailSettings.From));
            emailMessage.To.Add(new MailboxAddress("", sendEmail.Email));
            emailMessage.Subject = sendEmail.Subject;
            emailMessage.Body = new TextPart("html") { Text = sendEmail.Body };

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
