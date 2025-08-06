using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Constants;
using Domain.Interfaces;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService 
    {
        private readonly IApplicationMapper _mapper;
        private readonly IValidatorService _validatorService;
        private readonly IIdentityUserService _identityUserService;
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageBrokerService _messageBrokerService;

        public AuthService(
            IApplicationMapper mapper,
            IValidatorService validatorService,
            IIdentityUserService identityUserService,
            IJwtService jwtService,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMessageBrokerService messageBrokerService
            )
        {
            _mapper = mapper;
            _validatorService = validatorService;
            _identityUserService = identityUserService;
            _jwtService = jwtService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _messageBrokerService = messageBrokerService;
        }

        public async Task<Result> RegisterAsync(RegisterUserDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var mapToUser = _mapper.User.ToUser(dto);

            await _unitOfWork.BeginTransactionAsync();

            var userIdentityResult = await _identityUserService.AddAsync(mapToUser, dto.Password);

            if (!userIdentityResult.Success)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure(userIdentityResult.Message);
            }

            var roleAssignedResult = await _identityUserService.AssignRoleAsync(userIdentityResult.Data.IdentityId, Roles.User);

            if (!roleAssignedResult.Success)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure(roleAssignedResult.Message);
            }

            var mapToCreateUser = _mapper.User.ToCreateUser(userIdentityResult.Data.IdentityId, mapToUser);

            var createUserResult = await _userRepository.AddAsync(mapToCreateUser);

            if (!createUserResult.Success)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure(createUserResult.Message);
            }

            var MapToUserRegisteredEvent = _mapper.User.ToUserRegistered(createUserResult.Data);

            await _messageBrokerService.PublishAsync(MapToUserRegisteredEvent);

            await _unitOfWork.CommitAsync();

            return Result.Ok($"Usuário criado com sucesso. Um E-mail de Confirmação sera enviado para {MapToUserRegisteredEvent.Email}.");

        }

        public async Task<Result> ConfirmEmailAsync(ConfirmEmailDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var decodedToken = Uri.UnescapeDataString(dto.Token);

            var confirmationResult = await _identityUserService.ConfirmEmailAsync(dto.IdentityId, decodedToken);

            if (!confirmationResult.Success) 
                return Result.Failure(confirmationResult.Message);

            return Result.Ok(confirmationResult.Message);

        }

        public async Task<Result<string>> LoginAsync(LoginDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var checkResult = await _identityUserService.CheckPasswordAsync(dto.Email, dto.Password);

            if (!checkResult.Success)
                return Result<string>.Failure(checkResult.Message);

            var userResult = await _userRepository.FindByEmailAsync(checkResult.Data.Email);

            if (!userResult.Success)
                return Result<string>.Failure(userResult.Message);

            var rolesResult = await _identityUserService.GetRolesByIdAsync(userResult.Data.IdentityId);

            if (!rolesResult.Success)
                return Result<string>.Failure(rolesResult.Message);

            var token = _jwtService.GenerateToken(userResult.Data, rolesResult.Data);

            return Result<string>.Ok(token);

        }

        public async Task<Result> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var identityResult = await _identityUserService.FindByEmailAsync(dto.Email);

            if (!identityResult.Success)
                return Result.Failure(identityResult.Message);

            var mapToUserForgotPassword = _mapper.User.ToUserForgotPassword(identityResult.Data);

            await _messageBrokerService.PublishAsync(mapToUserForgotPassword);

            return Result.Ok($"E-mail de recuperação de senha enviado com sucesso para {identityResult.Data.Email}");

        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var resetPasswordResult = await _identityUserService.ResetPasswordAsync(dto.Email, dto.Token, dto.NewPassword);

            if (!resetPasswordResult.Success)
                return Result.Failure(resetPasswordResult.Message);

            return Result.Ok(resetPasswordResult.Message);
        }
    }
}
