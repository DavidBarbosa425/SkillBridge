using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Emails;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AuthService : IAuthService 
    {
        private readonly IApplicationMapper _mapper;
        private readonly IValidatorService _validatorService;
        private readonly IUserEmailConfirmationService _userEmailConfirmationService;
        private readonly IUserEmailPasswordResetService _userEmailPasswordResetService;
        private readonly IIdentityUserService _identityUserService;

        public AuthService(
            IApplicationMapper mapper,
            IValidatorService validatorService,
            IUserEmailConfirmationService userEmailConfirmationService,
            IUserEmailPasswordResetService userEmailPasswordResetService,
            IIdentityUserService identityUserService
            )
        {
            _mapper = mapper;
            _validatorService = validatorService;
            _userEmailConfirmationService = userEmailConfirmationService;
            _userEmailPasswordResetService = userEmailPasswordResetService;
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

            var sentEmail = await _userEmailConfirmationService.SendConfirmationEmailAsync(userDto);

            if (!sentEmail.Success)
                return Result.Failure(sentEmail.Message);

            return Result.Ok("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada.");

        }

        public async Task<Result> ConfirmEmailAsync(Guid userId, string token)
        {
            var decodedToken = Uri.UnescapeDataString(token);

            var confirmationResult = await _identityUserService.ConfirmEmailAsync(userId, decodedToken);

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
            var user = await _identityUserService.FindByEmailAsync(dto.Email);

            if (!user.Success)
                return Result.Failure("Se o e-mail estiver cadastrado, enviaremos um link de redefinição.");

            var userDto = _mapper.User.ToUserDto(user.Data!);

            await _userEmailPasswordResetService.SendEmailPasswordResetAsync(userDto);

            return Result.Ok("E-mail de recuperar senha enviado com sucesso! Confira sua caixa de entrada.");

        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var resetedPassword = await _identityUserService.ResetPasswordAsync(dto.Email, dto.Token, dto.NewPassword);

            if (!resetedPassword.Success)
                return Result.Failure(resetedPassword.Message);

            return Result.Ok(resetedPassword.Message);
        }
    }
}
