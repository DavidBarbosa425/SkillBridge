using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Identity.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInfrastructureMapper _infrastructureMapper;

        public UserRepository(
            UserManager<ApplicationUser> userManager,
            IInfrastructureMapper infrastructureMapper)
        {
            _userManager = userManager;
            _infrastructureMapper = infrastructureMapper;
        }
        public async Task<Result<User>> AddAsync(User user)
        {
            var applicationUser = _infrastructureMapper.User.ToApplicationUser(user);

            var creationResult = await _userManager.CreateAsync(applicationUser, user.Password);

            if (!creationResult.Succeeded)
            {
                var errors = creationResult.Errors.Select(e => e.Description);
                return Result<User>.Failure(string.Join("; ", errors));
            }

            var createdUser = _infrastructureMapper.User.ToUser(applicationUser);

            return Result<User>.Ok(createdUser);
        }

        public async Task<Result<User>> FindByIdAsync(string id)
        {

            var applicationUser = await _userManager.FindByIdAsync(id);

            if (applicationUser == null) return Result<User>.Failure("Usuário não encontrado");

            var user = _infrastructureMapper.User.ToUser(applicationUser);

            return Result<User>.Ok(user);
        }
    }
}
