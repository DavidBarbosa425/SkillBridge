using Application.DTOs;
using Application.Interfaces;
using Application.Templates;
using Domain.Common;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IEmailRepository _emailRepository;
        private readonly IUrlService _urlService;
        private readonly IValidatorService _validationRules;

        public AuthService(
            IUserRepository userRepository,
            IEmailService emailService,
            IEmailRepository emailRepository,
            IUrlService urlService,
            IValidatorService validationRules
            )
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _emailRepository = emailRepository;
            _urlService = urlService;
            _validationRules = validationRules;
        }

        public async Task<Result<string>> RegisterUserAsync(RegisterUserDto dto)
        {
            await _validationRules.ValidateAsync(dto);

            var user = ApplicationUserMapper.ToUser(dto);

            var creationResult = await _userRepository.AddAsync(user);

            if(!creationResult.Success) return Result<string>.Failure("Erro ao criar Usuário, tente novamente mais tarde");

            var result = await SendEmailConfirmationAsync(user);

            if(!result.Success) return Result<string>.Failure(result.Message);

            return Result<string>.Ok("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada.");

        }

        private async Task<Result<string>> SendEmailConfirmationAsync(User user)
        {
            var resultEmailBody = await GenerateEmailConfirmationAsync(user);

            if (!resultEmailBody.Success) return Result<string>.Failure(resultEmailBody.Message);

            var sendEmail = new SendEmail
            {
                Email = user.Email,
                Subject = EmailSubjects.Confirmation,
                Body = resultEmailBody.Data!
            };

            await _emailService.SendEmailAsync(sendEmail);

            return Result<string>.Ok("E-mail enviado com sucesso!");
        }

        private async Task<Result<string>> GenerateEmailConfirmationAsync(User user)
        {
            var confirmationLinkResult = await GenerateApiUrlConfirmationAsync(user.Email);

            if (!confirmationLinkResult.Success) return Result<string>.Failure(confirmationLinkResult.Message);

            var htmlBody = EmailTemplateFactory.GenerateConfirmationEmailHtml(user.Name, confirmationLinkResult.Data!);

            return Result<string>.Ok(htmlBody);
            
        }
        private async Task<Result<string>> GenerateApiUrlConfirmationAsync(string email)
        {

            var token = await _emailRepository.GenerateEmailConfirmationTokenAsync(email);

            if (string.IsNullOrEmpty(token)) return Result<string>.Failure("Erro ao gerar token de confirmação de e-mail.");

            var result = await _emailRepository.SaveTokenEmailConfirmationAsync(email, token);

            if (!result) return Result<string>.Failure("Erro ao salvar token de confirmação de e-mail.");

            var tokenGuid = await _emailRepository.GetEmailConfirmationTokenGuidAsync(email);

            if (tokenGuid == Guid.Empty) return Result<string>.Failure("Erro ao buscar token de confirmação de e-mail.");

            var confirmationLink = _urlService.GenerateApiUrl("auth", "confirmationUserEmail", new Dictionary<string, string?> { { "Guid", tokenGuid.ToString() } });

            return Result<string>.Ok(confirmationLink);
        }

        public async Task LoginAsync(LoginDto dto)
        {
            await _validationRules.ValidateAsync(dto);
        }
    }
}
