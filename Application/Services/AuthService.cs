using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Emails;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService 
    {
        private readonly IApplicationMapper _mapper;
        private readonly IValidatorService _validatorService;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IIdentityUserService _identityUserService;
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageBrokerService _messageBrokerService;

        public AuthService(
            IApplicationMapper mapper,
            IValidatorService validatorService,
            IEmailAccountService emailAccountService,
            IIdentityUserService identityUserService,
            IJwtService jwtService,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMessageBrokerService messageBrokerService
            )
        {
            _mapper = mapper;
            _validatorService = validatorService;
            _emailAccountService = emailAccountService;
            _identityUserService = identityUserService;
            _jwtService = jwtService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _messageBrokerService = messageBrokerService;
        }

        public async Task<Result> RegisterAsync(RegisterUserDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var user = _mapper.User.ToUser(dto);

            await _unitOfWork.BeginTransactionAsync();

            var createdUser = await _identityUserService.AddAsync(user, dto.Password);

            if (!createdUser.Success)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure(createdUser.Message);
            }

            var domainUserCreated = _userRepository.AddAsync(createdUser.Data!);

            if (!domainUserCreated.Success)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure(domainUserCreated.Message);
            }

            await _unitOfWork.CommitAsync();

            var userDto = _mapper.User.ToUserDto(createdUser.Data!);

            // Publicar mensagem no RabbitMQ em vez de enviar email diretamente
            var userRegisteredMessage = new UserRegisteredMessage
            {
                UserId = userDto.Id.ToString(),
                Email = userDto.Email,
                Name = userDto.Name
            };

            await _messageBrokerService.PublishAsync(userRegisteredMessage);

            //var userDto = _mapper.User.ToUserDto(createdUser.Data!);

            //var sentEmail = await _emailAccountService.SendConfirmationEmailAsync(userDto);

            //if (!sentEmail.Success)
            //{
            //    await _unitOfWork.RollbackAsync();
            //    return Result.Failure(sentEmail.Message);
            //}

            return Result.Ok($"Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para {userDto.Email}.");

        }

        public async Task<Result> ConfirmEmailAsync(ConfirmEmailDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var decodedToken = Uri.UnescapeDataString(dto.Token);

            var confirmationResult = await _identityUserService.ConfirmEmailAsync(dto.UserId, decodedToken);

            if (!confirmationResult.Success) return Result.Failure(confirmationResult.Message);

            return Result.Ok(confirmationResult.Message);

        }

        public async Task<Result<string>> LoginAsync(LoginDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var userChecked = await _identityUserService.CheckPasswordAsync(dto.Email, dto.Password);

            if (!userChecked.Success)
                return Result<string>.Failure(userChecked.Message);

            var token = await _jwtService.GenerateToken(userChecked.Data!);

            return Result<string>.Ok(token);

        }

        public async Task<Result> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var user = await _identityUserService.FindByEmailAsync(dto.Email);

            if (!user.Success)
                return Result.Failure(user.Message);

            var userDto = _mapper.User.ToUserDto(user.Data!);

            await _emailAccountService.SendPasswordResetEmailAsync(userDto);

            return Result.Ok($"E-mail de recuperação de senha enviado com sucesso para {user.Data?.Email}");

        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var passwordWasReset = await _identityUserService.ResetPasswordAsync(dto.Email, dto.Token, dto.NewPassword);

            if (!passwordWasReset.Success)
                return Result.Failure(passwordWasReset.Message);

            return Result.Ok(passwordWasReset.Message);
        }
    }
}
