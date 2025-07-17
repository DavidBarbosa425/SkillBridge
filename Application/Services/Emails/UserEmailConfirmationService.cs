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
    public class UserEmailConfirmationService : IUserEmailConfirmationService
    {
        private readonly IValidatorsService _validatorService;
        private readonly IIdentityUserService _identityUserService;
        private readonly IUrlService _urlService;
        private readonly IEmailTemplateFactory _emailTemplateFactory;
        private readonly IEmailService _emailService;

        public UserEmailConfirmationService(
            IValidatorsService validatorService,
            IIdentityUserService identityUserService,
            IUrlService urlService,
            IEmailTemplateFactory emailTemplateFactory,
            IEmailService emailService)
        {
            _validatorService = validatorService;
            _identityUserService = identityUserService;
            _urlService = urlService;
            _emailTemplateFactory = emailTemplateFactory;
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

            var body = _emailTemplateFactory.GenerateConfirmationEmailHtml(userDto.Name, confirmationLink);

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
        public async Task<Result> ConfirmEmailAsync(Guid userId, string token)
        {
            var decodedToken = Uri.UnescapeDataString(token);

            var confirmationResult = await _identityUserService.ConfirmEmailAsync(userId, decodedToken);

            if (!confirmationResult.Success) return Result.Failure(confirmationResult.Message);

            return Result.Ok(confirmationResult.Message);

        }
    }
}
