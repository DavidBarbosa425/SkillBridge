using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmailRepository(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> SaveTokenEmailConfirmationAsync(EmailConfirmationToken emailConfirmationToken)
        {
            _context.EmailConfirmationTokens.Add(emailConfirmationToken);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return token;
        }
        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }


    }
}
