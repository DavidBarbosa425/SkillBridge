using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Emails;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationMapper _mapper;
        private readonly IValidatorsService _validatorService;
        private readonly IUserEmailConfirmationService _userEmailConfirmationService;
        private readonly IIdentityUserService _identityUserService;

        public UserService(
            IApplicationMapper mapper,
            IValidatorsService validatorService,
            IUserEmailConfirmationService userEmailConfirmationService,
            IIdentityUserService identityUserService
            )
        {
            _mapper = mapper;
            _validatorService = validatorService;
            _userEmailConfirmationService = userEmailConfirmationService;
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
    }
}
