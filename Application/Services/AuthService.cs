using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Emails;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService 
    {
        private readonly IApplicationMapper _mapper;
        private readonly IValidatorService _validatorService;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IIdentityUserService _identityUserService;

        public AuthService(
            IApplicationMapper mapper,
            IValidatorService validatorService,
            IEmailAccountService emailAccountService,
            IIdentityUserService identityUserService
            )
        {
            _mapper = mapper;
            _validatorService = validatorService;
            _emailAccountService = emailAccountService;
            _identityUserService = identityUserService;

        }

        public async Task<Result> RegisterAsync(RegisterUserDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var user = _mapper.User.ToUser(dto);

            var createdUser = await _identityUserService.AddAsync(user);

            if (!createdUser.Success)
                return Result.Failure(createdUser.Message);

            var userDto = _mapper.User.ToUserDto(createdUser.Data!);

            var sentEmail = await _emailAccountService.SendConfirmationEmailAsync(userDto);

            if (!sentEmail.Success)
                return Result.Failure(sentEmail.Message);

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

        public async Task<Result<UserDto>> LoginAsync(LoginDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var userChecked = await _identityUserService.CheckPasswordAsync(dto.Email, dto.Password);

            if (!userChecked.Success)
                return Result<UserDto>.Failure(userChecked.Message);

            var user = _mapper.User.ToUserDto(userChecked.Data!);

            return Result<UserDto>.Ok(user);

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
