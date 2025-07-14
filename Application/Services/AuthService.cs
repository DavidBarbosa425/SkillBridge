using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationMapper _mapper;
        private readonly IValidatorService _validatorService;
        private readonly IEmailService _emailService;
        private readonly IEmailConfirmationService _emailConfirmationService;
        private readonly IEmailRepository _emailRepository;

        public AuthService(
            IUserRepository userRepository,
            IApplicationMapper mapper,
            IValidatorService validatorService,
            IEmailService emailService,
            IEmailConfirmationService emailConfirmationService,
            IEmailRepository emailRepository
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validatorService = validatorService;
            _emailService = emailService;
            _emailConfirmationService = emailConfirmationService;
            _emailRepository = emailRepository;
        }

        public async Task<Result> RegisterUserAsync(RegisterUserDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var user = _mapper.User.ToUser(dto);

            var createdUser = await _userRepository.AddAsync(user);

            if (!createdUser.Success)
                return Result.Failure("Erro ao criar usuário.");

            var userDto = _mapper.User.ToUserDto(createdUser.Data!);

            var sendEmail = await _emailConfirmationService.GenerateEmailConfirmation(userDto);

            if (!sendEmail.Success)
                return Result.Failure("Erro ao gerar e enviar e-mail de confirmação.");

            await _emailService.SendEmailAsync(sendEmail.Data!);

            return Result.Ok("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada.");

        }

        public async Task<Result> ConfirmationUserEmailAsync(Guid id)
        {
            var token = await _emailRepository.GetEmailConfirmationTokenAsync(id);

            if (!token.Success) return Result.Failure(token.Message);

            var user = await _userRepository.FindByIdAsync(token.Data!.UserId);

            if (!user.Success) return Result.Failure(user.Message);

            var confirmationResult = await _emailService.ConfirmationUserEmailAsync(user.Data!, token.Data.Token);

            if (!confirmationResult.Success) return Result.Failure(confirmationResult.Message);

            var removeTokenResult = await _emailRepository.RemoveTokenConfirmationUserEmailAsync(token.Data);

            if (!removeTokenResult.Success) return Result.Failure(removeTokenResult.Message);

            return Result.Ok("E-mail confirmado com sucesso!");

        }


        public async Task LoginAsync(LoginDto dto)
        {
            await _validatorService.ValidateAsync(dto);
        }
    }
}
