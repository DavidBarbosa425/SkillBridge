using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApplicationMapper _mapper;
        private readonly IValidatorService _validatorService;
        private readonly IEmailService _emailService;
        private readonly IIdentityUserService _identityUserService;
        private readonly IUrlService _urlService;
        private readonly IEmailTemplateFactory _emailTemplateFactory;

        public AuthService(
            IApplicationMapper mapper,
            IValidatorService validatorService,
            IEmailService emailService,
            IIdentityUserService identityUserService,
            IUrlService urlService,
            IEmailTemplateFactory emailTemplateFactory
            )
        {
            _mapper = mapper;
            _validatorService = validatorService;
            _emailService = emailService;
            _identityUserService = identityUserService;
            _urlService = urlService;
            _emailTemplateFactory = emailTemplateFactory;
        }

        public async Task<Result> RegisterUserAsync(RegisterUserDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var user = _mapper.User.ToUser(dto);

            var createdUser = await _identityUserService.AddAsync(user);

            if (!createdUser.Success)
                return Result.Failure("Erro ao criar usuário.");

            var userDto = _mapper.User.ToUserDto(createdUser.Data!);

            var sendEmail = await GenerateEmailConfirmation(userDto);

            if (!sendEmail.Success)
                return Result.Failure("Erro ao gerar e enviar e-mail de confirmação.");

            await _emailService.SendEmailAsync(sendEmail.Data!);

            return Result.Ok("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada.");

        }
        public async Task<Result<SendEmail>> GenerateEmailConfirmation(UserDto userDto)
        {
            await _validatorService.ValidateAsync(userDto);

            var token = await _identityUserService.GenerateEmailConfirmationTokenAsync(userDto.Id);

            if (!token.Success)
                return Result<SendEmail>.Failure(token.Message);

            var tokenEncoded = Uri.EscapeDataString(token.Data!);

            var confirmationLink = _urlService.GenerateApiUrl("auth", "confirmationUserEmail",
                new Dictionary<string, string?> { { "userId", userDto.Id.ToString() }, { "token", tokenEncoded } });

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
        public async Task LoginAsync(LoginDto dto)
        {
            await _validatorService.ValidateAsync(dto);
        }
    }
}
