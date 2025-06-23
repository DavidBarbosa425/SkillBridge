using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailRepository _emailRepository;

        public EmailService(
            IHttpContextAccessor httpContextAccessor,
            IEmailRepository emailRepository
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _emailRepository = emailRepository;
        }

        public IEmailRepository EmailRepository { get; }

        public async void SendRegistrationConfirmationAsync(string name, string email)
        {
            var applicationUser = new ApplicationUser(name, email);

            var body = await GenerateBody(applicationUser, EmailSubjects.Confirmation);
        }

        private async Task<string> GenerateBody(ApplicationUser user, string subject)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            var scheme = request?.Scheme ?? "https";
            var host = request?.Host.ToString() ?? "localhost";

            var htmlMessage = string.Empty;

            if (subject == EmailSubjects.Confirmation)
            {
                var token = await _emailRepository.GenerateEmailConfirmationTokenAsync(user);
                var emailConfirmationToken = new EmailConfirmationToken(user, token);
                var result = await _emailRepository.SaveTokenEmailConfirmationAsync(emailConfirmationToken);

                var confirmationLink = $"{scheme}://{host}/api/auth/confirmUserEmail?id={emailConfirmationToken.Id}";

                htmlMessage = $@"
                <p>Olá {emailConfirmationToken.Name},</p>
                <p>Clique no botão abaixo para confirmar seu e-mail:</p>
                <p><a style='padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none;' href='{confirmationLink}'>Confirmar E-mail</a></p>
                <p>Se você não se registrou, ignore este e-mail.</p>
                 ";

                return htmlMessage;
            }


            return htmlMessage;

        }
    }
}
