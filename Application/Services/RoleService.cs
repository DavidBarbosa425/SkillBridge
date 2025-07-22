using Application.DTOs;
using Application.Interfaces;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IIdentityUserService _identityUserService;

        public RoleService(IIdentityUserService identityUserService)
        {
            _identityUserService = identityUserService;
        }
        public async Task<Result> AssignRoleToUserAsync(RoleAssignDto dto)
        {
            var result = await _identityUserService.AssignRoleToUserAsync(dto.Email, dto.Role);

            if (!result.Success)
                return Result.Failure(result.Message);

            return Result.Ok(result.Message);

        }
    }
}
