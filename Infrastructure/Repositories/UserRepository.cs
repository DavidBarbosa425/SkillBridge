using Domain.Entities;
using Domain.Interfaces;
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
        public async Task<string> AddAsync(string name, string email, string password)
        {
            var applicationUser = new ApplicationUser(name, email);

            var creationResult = await _userManager.CreateAsync(applicationUser, password);

            return "teste";
        }
    }
}
