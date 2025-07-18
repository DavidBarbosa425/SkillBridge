using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Emails;
using Application.Interfaces.Factories;
using Domain.Common;
using Domain.Constants;
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
        public async Task<Result> SendConfirmationEmailAsync(UserDto userDto)
        {
            await _validatorService.ValidateAsync(userDto);

            var token = await _identityUserService.GenerateEmailConfirmationTokenAsync(userDto.Id);

            if (!token.Success)
                return Result.Failure(token.Message);

            var confirmationLink = _urlService.GenerateApiUrlEmailConfirmation(userDto.Id.ToString(), token.Data!);

            if (string.IsNullOrEmpty(confirmationLink))
                return Result.Failure("Falha ao gerar link de confirmação de e-mail.");

            var body = _accountEmailTemplateFactory.GenerateConfirmationEmailHtml(userDto.Name, confirmationLink);

            if (string.IsNullOrEmpty(body))
                return Result.Failure("Falha ao gerar texto de confirmação de e-mail.");

            var sendEmail = new SendEmail
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Subject = EmailSubjects.Confirmation,
                Body = body
            };

            await _emailService.SendEmailAsync(sendEmail);

            return Result.Ok($"E-mail enviado com sucesso para {sendEmail.Email}");
        }

        public async Task<Result> SendPasswordResetEmailAsync(UserDto userDto)
        {
            await _validatorService.ValidateAsync(userDto);

            var token = await _identityUserService.GeneratePasswordResetTokenAsync(userDto.Id);

            if (!token.Success)
                return Result.Failure(token.Message);

            var resetPasswordLink = _urlService.GenerateApiUrlEmailPasswordReset(userDto.Id.ToString(), token.Data!);

            if (string.IsNullOrEmpty(resetPasswordLink))
                return Result.Failure("Falha ao gerar link de reset de senha.");

            var body = _accountEmailTemplateFactory.GeneratePasswordResetEmailHtml(userDto.Name, resetPasswordLink);

            if (string.IsNullOrEmpty(body))
                return Result.Failure("Falha ao gerar texto de reset de senha.");

            var sendEmail = new SendEmail
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Subject = EmailSubjects.PasswordReset,
                Body = body
            };

            await _emailService.SendEmailAsync(sendEmail);

            return Result.Ok($"E-mail enviado com sucesso para {sendEmail.Email}");
        }

    }
}
