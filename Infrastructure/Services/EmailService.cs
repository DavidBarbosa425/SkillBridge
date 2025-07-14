using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Configurations;
using Infrastructure.Identity.Models;
using Infrastructure.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;


namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly SmtpSettings _smtpSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInfrastructureMapper _infrastructureMapper;

        public EmailService(
            IOptions<EmailSettings> emailOptions,
            IOptions<SmtpSettings> smtpOptions,
            UserManager<ApplicationUser> userManager,
            IInfrastructureMapper infrastructureMapper
            )
        {
            _emailSettings = emailOptions.Value;
            _smtpSettings = smtpOptions.Value;
            _userManager = userManager;
            _infrastructureMapper = infrastructureMapper;
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

        public async Task<Result> ConfirmationUserEmailAsync(User user, string token)
        {
            var applicationUser = _infrastructureMapper.User.ToApplicationUser(user);

            var result = await _userManager.ConfirmEmailAsync(applicationUser, token);

            if (!result.Succeeded) return Result.Failure("Erro ao confirmar o e-mail do usuário.");

            return Result.Ok("E-mail confirmado com sucesso!");

        }

        public async Task<Result<string>> GenerateEmailConfirmationTokenAsync(User user)
        {
            var userMapper = _infrastructureMapper.User.ToApplicationUser(user);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userMapper);

            if (string.IsNullOrEmpty(token))
                return Result<string>.Failure("Falha ao gerar token de confirmação de e-mail.");

            return Result<string>.Ok(token);
        }

        public async Task<Result<string>> GeneratePasswordResetTokenAsync(User user)
        {
            var applicationUser = _infrastructureMapper.User.ToApplicationUser(user);

            var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);

            if (string.IsNullOrEmpty(token))
                return Result<string>.Failure("Falha ao gerar token de redefinição de senha.");

            return Result<string>.Ok(token);
        }

    }
}
