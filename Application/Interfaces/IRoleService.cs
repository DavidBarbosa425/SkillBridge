using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<Result> AssignRoleToUserAsync(RoleAssignDto roleAssignDto);
    }
}
