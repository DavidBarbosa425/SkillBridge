using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<bool> SaveTokenEmailConfirmationAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return false;

            var emailConfirmationToken = new EmailConfirmationToken
            {
                UserId = user.Id,
                Name = user.UserName!,
                Email = user.Email!,
                Token = token
            };

            _context.EmailConfirmationTokens.Add(emailConfirmationToken);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Guid> GetEmailConfirmationTokenGuidAsync(string email)
        {
            var result = await _context.EmailConfirmationTokens.FirstOrDefaultAsync(t => t.Email.Contains(email));
            return result!.Id;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user!);

            return token;
        }
        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user!);

            return token;
        }


    }
}
