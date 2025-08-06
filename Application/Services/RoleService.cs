using Application.DTOs;
using Application.Interfaces;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IIdentityUserService _identityUserService;
        private readonly IValidatorService _validatorService;

        public RoleService(
            IIdentityUserService identityUserService,
            IValidatorService validatorService)
        {
            _identityUserService = identityUserService;
            _validatorService = validatorService;
        }
        public async Task<Result> AssignRoleToUserAsync(RoleAssignDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var result = await _identityUserService.AssignRoleAsync(dto.IdentityUserId, dto.Role);

            if (!result.Success)
                return Result.Failure(result.Message);

            return Result.Ok(result.Message);

        }
    }
}
