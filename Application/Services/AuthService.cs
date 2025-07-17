using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService 
    {
        private readonly IValidatorsService _validatorService;
        private readonly IIdentityUserService _identityUserService;
        private readonly IApplicationMapper _applicationMapper;

        public AuthService(
            IValidatorsService validatorService,
            IIdentityUserService identityUserService,
            IApplicationMapper applicationMapper
            )
        {
            _validatorService = validatorService;
            _identityUserService = identityUserService;
            _applicationMapper = applicationMapper;
        }
     
        public async Task<Result<UserDto>> LoginAsync(LoginDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var userChecked = await _identityUserService.CheckPasswordAsync(dto.Email, dto.Password);

            if (!userChecked.Success)
                return Result<UserDto>.Failure(userChecked.Message);

            var user = _applicationMapper.User.ToUserDto(userChecked.Data!);

            return Result<UserDto>.Ok(user);

        }
    }
}
