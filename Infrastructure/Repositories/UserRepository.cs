using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Identity.Models;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<string>> AddAsync(User user)
        {
            var applicationUser = InfrastructureUserMapper.ToApplicationUser(user);

            var creationResult = await _userManager.CreateAsync(applicationUser, user.Password);

            if (!creationResult.Succeeded)
            {
                var errors = creationResult.Errors.Select(e => e.Description).ToList();
                return Result<string>.Failure(errors);
            }

            return Result<string>.Ok(applicationUser.Id);
        }
    }
}
