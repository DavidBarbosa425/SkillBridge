using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationMapper _mapper;
        private readonly IValidatorService _validationRules;
        private readonly IEmailService _emailService;
        private readonly IEmailConfirmationService _emailConfirmationService;

        public AuthService(
            IUserRepository userRepository,
            IApplicationMapper mapper,
            IValidatorService validationRules,
            IEmailService emailService,
            IEmailConfirmationService emailConfirmationService
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validationRules = validationRules;
            _emailService = emailService;
            _emailConfirmationService = emailConfirmationService;
        }

        public async Task<Result<string>> RegisterUserAsync(RegisterUserDto dto)
        {
            await _validationRules.ValidateAsync(dto);

            var user = _mapper.User.ToUser(dto);

            var creationResult = await _userRepository.AddAsync(user);

            if(!creationResult.Success) return Result<string>.Failure("Erro ao criar Usuário, tente novamente mais tarde");

            var sendEmail = await _emailConfirmationService.GenerateEmailConfirmation(user);

            await _emailService.SendEmailAsync(sendEmail);

            return Result<string>.Ok("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada.");

        }

        public async Task LoginAsync(LoginDto dto)
        {
            await _validationRules.ValidateAsync(dto);
        }
    }
}
