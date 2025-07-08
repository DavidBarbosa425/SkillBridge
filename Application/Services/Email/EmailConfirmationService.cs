using Application.Interfaces;
using Application.Templates;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Email
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IUrlService _urlService;

        public EmailConfirmationService(
            IEmailRepository emailRepository,
            IUrlService urlService)
        {
            _emailRepository = emailRepository;
            _urlService = urlService;
        }
        public async Task<SendEmail> GenerateEmailConfirmation(User user)
        {
            var token = await _emailRepository.GenerateEmailConfirmationTokenAsync(user.Email);

            if (string.IsNullOrEmpty(token))
                throw new Exception("Falha ao gerar Token de confirmação por e-mail.");

            var result = await _emailRepository.SaveTokenEmailConfirmationAsync(user.Email, token);

            if (!result)
                throw new Exception("Falha ao salvar Token de confirmação por e-mail.");

            var tokenGuid = await _emailRepository.GetEmailConfirmationTokenGuidAsync(user.Email);

            if (tokenGuid == Guid.Empty)
                throw new Exception("Falha ao gerar GUID de confirmação por e-mail.");

            var confirmationLink = _urlService.GenerateApiUrl("auth", "confirmationUserEmail",
                new Dictionary<string, string?> { { "Guid", tokenGuid.ToString() } });

            if (string.IsNullOrEmpty(confirmationLink))
                throw new Exception("Falha ao gerar link de confirmação de e-mail.");

            var body = EmailTemplateFactory.GenerateConfirmationEmailHtml(user.Name, confirmationLink!);

            if (string.IsNullOrEmpty(body))
                throw new Exception("Falha ao gerar texto de confirmação de e-mail.");

            return new SendEmail
            {
                Name = user.Name,
                Email = user.Email,
                Subject = EmailSubjects.Confirmation,
                Body = body
            };
        }
    }
}
