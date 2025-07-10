using Domain.Common;
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

        public async Task<Result<string>> GenerateEmailConfirmationTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Result<string>.Failure("Usuário não encontrado.");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            if (string.IsNullOrEmpty(token))
                return Result<string>.Failure("Falha ao gerar token de confirmação de e-mail.");

            return Result<string>.Ok(token);
        }

        public async Task<Result> SaveTokenEmailConfirmationAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) Result.Failure("Erro ao buscar usuário para salvar token de confirmação");

            var emailConfirmationToken = new EmailConfirmationToken
            {
                UserId = user!.Id,
                Name = user.UserName!,
                Email = user.Email!,
                Token = token
            };

            _context.EmailConfirmationTokens.Add(emailConfirmationToken);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) Result.Failure("Erro ao salvar token de confirmação de e-mail.");

            return Result.Ok();
        }

        public async Task<Result<Guid>> GetEmailConfirmationTokenGuidAsync(string email)
        {
            var result = await _context.EmailConfirmationTokens.FirstOrDefaultAsync(t => t.Email.Contains(email));

            if( result == null)
                return Result<Guid>.Failure("Token de confirmação de e-mail não encontrado.");

            return Result<Guid>.Ok(result.Id);

        }

        public async Task<Result<string>> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Result<string>.Failure("Usuário não encontrado.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (string.IsNullOrEmpty(token))
                return Result<string>.Failure("Falha ao gerar token de redefinição de senha.");

            return Result<string>.Ok(token);
        }
    }
}
