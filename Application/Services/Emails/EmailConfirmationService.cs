using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Emails
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IIdentityUserService _identityUserService;
        private readonly IUrlService _urlService;
        private readonly IEmailTemplateFactory _emailTemplateFactory;
        private readonly IValidatorService _validatorService;
        private readonly IApplicationMapper _applicationMapper;

        public EmailConfirmationService(
            IIdentityUserService identityUserService,
            IUrlService urlService,
            IEmailTemplateFactory emailTemplateFactory,
            IValidatorService validatorService,
            IApplicationMapper applicationMapper)
        {
            _identityUserService = identityUserService;
            _urlService = urlService;
            _emailTemplateFactory = emailTemplateFactory;
            _validatorService = validatorService;
            _applicationMapper = applicationMapper;
        }
        public async Task<Result<SendEmail>> GenerateEmailConfirmation(UserDto userDto)
        {
            await _validatorService.ValidateAsync(userDto);

            var token = await _identityUserService.GenerateEmailConfirmationTokenAsync(userDto.Id);

            if (!token.Success)
                return Result<SendEmail>.Failure(token.Message);

            var tokenEncoded = Uri.EscapeDataString(token.Data!);

            var confirmationLink = _urlService.GenerateApiUrl("auth", "confirmationUserEmail",
                new Dictionary<string, string?> { {"userId", userDto.Id.ToString() }, { "token", tokenEncoded } });

            if (string.IsNullOrEmpty(confirmationLink))
                return Result<SendEmail>.Failure("Falha ao gerar link de confirmação de e-mail.");

            var body = _emailTemplateFactory.GenerateConfirmationEmailHtml(userDto.Name, confirmationLink!);

            if (string.IsNullOrEmpty(body))
                return Result<SendEmail>.Failure("Falha ao gerar texto de confirmação de e-mail.");

            var sendEmail = new SendEmail
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Subject = EmailSubjects.Confirmation,
                Body = body
            };

            return Result<SendEmail>.Ok(sendEmail);
        }
        public async Task<Result> ConfirmationUserEmailAsync(Guid userId, string token)
        {
            var decodedToken = Uri.UnescapeDataString(token);

            var confirmationResult = await _identityUserService.ConfirmationUserEmailAsync(userId, decodedToken);

            if (!confirmationResult.Success) return Result.Failure(confirmationResult.Message);

            return Result.Ok("E-mail confirmado com sucesso!");

        }

    }
}
