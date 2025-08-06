using Domain.Entities;
using Infrastructure.Configurations;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Domain.Interfaces;


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

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailSettings.Sender, _emailSettings.From));
            mimeMessage.To.Add(new MailboxAddress("", emailMessage.To));
            mimeMessage.Subject = emailMessage.Subject;
            mimeMessage.Body = new TextPart("html") { Text = emailMessage.Body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }

        }

    }
}
