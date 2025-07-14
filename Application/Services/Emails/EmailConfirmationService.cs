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
        private readonly IEmailRepository _emailRepository;
        private readonly IUrlService _urlService;
        private readonly IEmailTemplateFactory _emailTemplateFactory;
        private readonly IValidatorService _validatorService;
        private readonly IApplicationMapper _applicationMapper;
        private readonly IEmailService _emailService;

        public EmailConfirmationService(
            IEmailRepository emailRepository,
            IUrlService urlService,
            IEmailTemplateFactory emailTemplateFactory,
            IValidatorService validatorService,
            IApplicationMapper applicationMapper,
            IEmailService emailService)
        {
            _emailRepository = emailRepository;
            _urlService = urlService;
            _emailTemplateFactory = emailTemplateFactory;
            _validatorService = validatorService;
            _applicationMapper = applicationMapper;
            _emailService = emailService;
        }
        public async Task<Result<SendEmail>> GenerateEmailConfirmation(UserDto userDto)
        {
            await _validatorService.ValidateAsync(userDto);

            var user = _applicationMapper.User.ToUser(userDto);

            var token = await _emailService.GenerateEmailConfirmationTokenAsync(user);

            if (!token.Success)
                return Result<SendEmail>.Failure(token.Message);

            var savedToken = await _emailRepository.SaveTokenEmailConfirmationAsync(user, token.Data.ToUpper()!);

            if (!savedToken.Success)
                return Result<SendEmail>.Failure(savedToken.Message);

            var confirmationLink = _urlService.GenerateApiUrl("auth", "confirmationUserEmail",
                new Dictionary<string, string?> { { "Id", savedToken.Data!.Id.ToString().ToUpper() } });

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
