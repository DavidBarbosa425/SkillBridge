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
        public async Task<User> AddAsync(User user)
        {
            var applicationUser = _infrastructureMapper.User.ToApplicationUser(user);

            var creationResult = await _userManager.CreateAsync(applicationUser, user.Password);

            if (!creationResult.Succeeded)
            {
                var errors = creationResult.Errors.Select(e => e.Description);
                throw new Exception(errors.ToString());
            }

            var createdUser = _infrastructureMapper.User.ToUser(applicationUser);

            return createdUser;
        }
    }
}
