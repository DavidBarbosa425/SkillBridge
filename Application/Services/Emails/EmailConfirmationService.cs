using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Factories;
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
        public async Task<SendEmail> GenerateEmailConfirmation(UserDto userDto)
        {
            await _validatorService.ValidateAsync(userDto);

            var token = await _emailRepository.GenerateEmailConfirmationTokenAsync(userDto.Email);

            if (string.IsNullOrEmpty(token))
                throw new Exception("Falha ao gerar Token de confirmação por e-mail.");

            var result = await _emailRepository.SaveTokenEmailConfirmationAsync(userDto.Email, token);

            if (!result)
                throw new Exception("Falha ao salvar Token de confirmação por e-mail.");

            var tokenGuid = await _emailRepository.GetEmailConfirmationTokenGuidAsync(userDto.Email);

            if (tokenGuid == Guid.Empty)
                throw new Exception("Falha ao gerar GUID de confirmação por e-mail.");

            var confirmationLink = _urlService.GenerateApiUrl("auth", "confirmationUserEmail",
                new Dictionary<string, string?> { { "Guid", tokenGuid.ToString() } });

            if (string.IsNullOrEmpty(confirmationLink))
                throw new Exception("Falha ao gerar link de confirmação de e-mail.");

            var body = _emailTemplateFactory.GenerateConfirmationEmailHtml(userDto.Name, confirmationLink!);

            if (string.IsNullOrEmpty(body))
                throw new Exception("Falha ao gerar texto de confirmação de e-mail.");

            return new SendEmail
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Subject = EmailSubjects.Confirmation,
                Body = body
            };
        }
    }
}
