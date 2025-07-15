using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Identity.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInfrastructureMapper _infrastructureMapper;

        public IdentityUserService(
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

        public async Task<Result<string>> GenerateEmailConfirmationTokenAsync(Guid userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId.ToString());

            if (applicationUser == null)
                return Result<string>.Failure("Usuário não encontrado.");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);

            if (string.IsNullOrEmpty(token))
                return Result<string>.Failure("Falha ao gerar token de confirmação de e-mail.");

            return Result<string>.Ok(token);
        }

        public async Task<Result> ConfirmationUserEmailAsync(Guid userId, string token)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId.ToString());

            if (applicationUser == null)
                return Result.Failure("Usuário não encontrado.");

            var result = await _userManager.ConfirmEmailAsync(applicationUser, token);

            if (!result.Succeeded) return Result.Failure("Erro ao confirmar o e-mail do usuário.");

            return Result.Ok("E-mail confirmado com sucesso!");

        }

        public async Task<Result<string>> GeneratePasswordResetTokenAsync(Guid userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId.ToString());

            if (applicationUser == null)
                return Result<string>.Failure("Usuário não encontrado.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);

            if (string.IsNullOrEmpty(token))
                return Result<string>.Failure("Falha ao gerar token de redefinição de senha.");

            return Result<string>.Ok(token);
        }
    }
}
