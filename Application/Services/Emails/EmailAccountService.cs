using Application.Constants;
using Application.Interfaces;
using Application.Interfaces.Emails;
using Application.Interfaces.Factories;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Emails
{
    public class EmailAccountService : IEmailAccountService
    {
        private readonly IValidatorService _validatorService;
        private readonly IIdentityUserService _identityUserService;
        private readonly IUrlService _urlService;
        private readonly IAccountEmailTemplateFactory _accountEmailTemplateFactory;
        private readonly IEmailService _emailService;

        public EmailAccountService(
            IValidatorService validatorService,
            IIdentityUserService identityUserService,
            IUrlService urlService,
            IAccountEmailTemplateFactory accountEmailTemplateFactory,
            IEmailService emailService)
        {
            _validatorService = validatorService;
            _identityUserService = identityUserService;
            _urlService = urlService;
            _accountEmailTemplateFactory = accountEmailTemplateFactory;
            _emailService = emailService;
        }
        public async Task<Result> SendConfirmationEmailAsync(UserRegistered user)
        {
            await _validatorService.ValidateAsync(user);

            var token = await _identityUserService.GenerateEmailConfirmationTokenAsync(user.IdentityId);

            if (!token.Success)
                return Result.Failure(token.Message);

            var confirmationLink = _urlService.GenerateApiUrlEmailConfirmation(user.IdentityId, token.Data);

            if (string.IsNullOrEmpty(confirmationLink))
                return Result.Failure("Falha ao gerar link de confirmação de e-mail.");

            var body = _accountEmailTemplateFactory.GenerateConfirmationEmailHtml(user.Name, confirmationLink);

            if (string.IsNullOrEmpty(body))
                return Result.Failure("Falha ao gerar texto de confirmação de e-mail.");

            var sendEmail = new SendEmail
            {
                Name = user.Name,
                Email = user.Email,
                Subject = EmailSubjects.Confirmation,
                Body = body
            };

            await _emailService.SendEmailAsync(sendEmail);

            return Result.Ok($"Um e-mail de confirmação sera enviado para {user.Email}");
        }

        public async Task<Result> SendPasswordResetEmailAsync(UserForgotPassword user)
        {
            await _validatorService.ValidateAsync(user);

            var token = await _identityUserService.GeneratePasswordResetTokenAsync(user.IdentityId);

            if (!token.Success)
                return Result.Failure(token.Message);

            var resetPasswordLink = _urlService.GenerateUrlEmailPasswordReset(user.IdentityId, token.Data!);

            if (string.IsNullOrEmpty(resetPasswordLink))
                return Result.Failure("Falha ao gerar link de reset de senha.");

            var body = _accountEmailTemplateFactory.GeneratePasswordResetEmailHtml(user.Name, resetPasswordLink);

            if (string.IsNullOrEmpty(body))
                return Result.Failure("Falha ao gerar texto de reset de senha.");

            var sendEmail = new SendEmail
            {
                Name = user.Name,
                Email = user.Email,
                Subject = EmailSubjects.PasswordReset,
                Body = body
            };

            await _emailService.SendEmailAsync(sendEmail);

            return Result.Ok($"Um e-mail para reset de senha sera enviado para {user.Email}");
        }

    }
}
