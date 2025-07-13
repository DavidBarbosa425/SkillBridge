using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Domain.Common;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Emails
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IUrlService _urlService;
        private readonly IEmailTemplateFactory _emailTemplateFactory;
        private readonly IValidatorService _validatorService;

        public EmailConfirmationService(
            IEmailRepository emailRepository,
            IUrlService urlService,
            IEmailTemplateFactory emailTemplateFactory,
            IValidatorService validatorService)
        {
            _emailRepository = emailRepository;
            _urlService = urlService;
            _emailTemplateFactory = emailTemplateFactory;
            _validatorService = validatorService;
        }
        public async Task<Result<SendEmail>> GenerateEmailConfirmation(UserDto userDto)
        {
            await _validatorService.ValidateAsync(userDto);

            var token = await _emailRepository.GenerateEmailConfirmationTokenAsync(userDto.Email);

            if (!token.Success)
                return Result<SendEmail>.Failure("Falha ao gerar Token de confirmação por e-mail.");

            var result = await _emailRepository.SaveTokenEmailConfirmationAsync(userDto.Email, token.Data!);

            if (!result.Success)
                return Result<SendEmail>.Failure("Falha ao salvar Token de confirmação por e-mail.");

            var tokenGuid = await _emailRepository.GetEmailConfirmationTokenGuidAsync(userDto.Email);

            if (!tokenGuid.Success)
                return Result<SendEmail>.Failure("GUID de confirmação por e-mail não foi achado.");

            var confirmationLink = _urlService.GenerateApiUrl("auth", "confirmationUserEmail",
                new Dictionary<string, string?> { { "Id", tokenGuid.Data.ToString() } });

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
    }
}
