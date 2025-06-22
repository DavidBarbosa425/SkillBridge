using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmailService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void SendRegistrationConfirmationAsync(string name, string email)
        {
            var applicationUser = new ApplicationUser(name, email);

            EmailConfirmationAsync(applicationUser);
        }

        private async Task EmailConfirmationAsync(ApplicationUser user)
        {
            await GenerateMessage(user, EmailSubjects.Confirmation);
        }

        private async Task<string> GenerateMessage(ApplicationUser user, string subject)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            var scheme = request?.Scheme ?? "https";
            var host = request?.Host.ToString() ?? "localhost";

            var htmlMessage = string.Empty;

            if (subject == EmailSubjects.Confirmation)
            {
                var token = "";
                var emailConfirmationToken = new EmailConfirmationToken(user, token);
                //await SaveEmailTokenAsync(emailConfirmToken);

                var confirmationLink = $"{scheme}://{host}/api/auth/confirmUserEmail?id={emailConfirmationToken.Id}";

                htmlMessage = $@"
                <p>Olá {emailConfirmationToken.Name},</p>
                <p>Clique no botão abaixo para confirmar seu e-mail:</p>
                <p><a style='padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none;' href='{confirmationLink}'>Confirmar E-mail</a></p>
                <p>Se você não se registrou, ignore este e-mail.</p>
                 ";

                return htmlMessage;
            }

            if (subject == EmailSubjects.PasswordReset)
            {
                var token = "";

                var emailConfirmationToken = new EmailConfirmationToken(user, token);

                //await SaveEmailTokenAsync(emailConfirmToken);

                var confirmationLink = $"{scheme}://{host}/api/auth/resetPassword?email={user.Email}&token={Uri.EscapeDataString(emailConfirmationToken.Token)}";

                htmlMessage = $@"
                <p>Olá {emailConfirmationToken.Name},</p>
                <p>Clique no botão abaixo para redefinir sua senha:</p>
                <p><a style='padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none;' href='{confirmationLink}'>Redefinir Senha</a></p>
                <p>Se você não pediu para redefinir sua senha, ignore este e-mail.</p>
             ";

                return htmlMessage;
            }

            return htmlMessage;

        }
    }
}
