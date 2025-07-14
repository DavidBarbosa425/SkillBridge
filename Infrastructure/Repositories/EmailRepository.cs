using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IInfrastructureMapper _infrastructureMapper;

        public EmailRepository(
            ApplicationDbContext context,
            IInfrastructureMapper infrastructureMapper)
        {
            _context = context;
            _infrastructureMapper = infrastructureMapper;
        }

        public async Task<Result<EmailConfirmationToken>> SaveTokenEmailConfirmationAsync(User user, string token)
        {

            var emailConfirmationToken = new EmailConfirmationToken
            {
                Id = Guid.NewGuid(),
                UserId = user!.Id.ToString().ToUpper(),
                Name = user.Name!,
                Email = user.Email!,
                Token = token
            };

            _context.EmailConfirmationTokens.Add(emailConfirmationToken);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<EmailConfirmationToken>.Failure("Erro ao salvar token de confirmação de e-mail.");

            return Result<EmailConfirmationToken>.Ok(emailConfirmationToken);
        }

        public async Task<Result<EmailConfirmationToken>> GetEmailConfirmationTokenAsync(Guid id)
        {
            var tokenEntry = await _context.EmailConfirmationTokens
                .FirstOrDefaultAsync(t => t.Id == id && t.Expiration > DateTime.UtcNow);

            if (tokenEntry == null)
                return Result<EmailConfirmationToken>.Failure("Token de confirmação inválido ou expirado.");

            return Result<EmailConfirmationToken>.Ok(tokenEntry);

        }

        public async Task<Result> RemoveTokenConfirmationUserEmailAsync(EmailConfirmationToken emailConfirmationToken)
        {

            _context.EmailConfirmationTokens.Remove(emailConfirmationToken);

            var saved = await _context.SaveChangesAsync() > 0;

            if (!saved) return Result.Failure("Erro ao remover o token de confirmação de e-mail.");

            return Result.Ok("Token de confirmação de e-mail removido com sucesso.");

        }

    }
}
