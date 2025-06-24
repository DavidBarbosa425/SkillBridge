using Application.DTOs;
using Application.Interfaces;
using Application.Templates;
using Domain.Common;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IEmailRepository _emailRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(
            IUserRepository userRepository,
            IEmailService emailService,
            IEmailRepository emailRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _emailRepository = emailRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<string>> RegisterUserAsync(RegisterUserDto dto)
        {
            var creationResult = await _userRepository.AddAsync(dto.Name, dto.Email, dto.Password);

            if(!creationResult.Success) return Result<string>.Failure("Erro ao criar Usuário, tente novamente mais tarde");

            var resultEmailBody = await GenerateEmailConfirmationBodyAsync(new ApplicationUser(dto.Name, dto.Email));

            if(!resultEmailBody.Success) return Result<string>.Failure(resultEmailBody.Message);

            await _emailService.SendEmailAsync(dto.Email, EmailSubjects.Confirmation, resultEmailBody.Data);

            return Result<string>.Ok("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada.");

        }

        private async Task<Result<string>> GenerateEmailConfirmationBodyAsync(ApplicationUser user)
        {
            var confirmationLinkResult = await GenerateConfirmationLinkAsync(user);

            if (!confirmationLinkResult.Success) return Result<string>.Failure(confirmationLinkResult.Message);

            var htmlBody = EmailTemplateFactory.CreateConfirmationEmail(user.UserName, confirmationLinkResult.Data);

            return Result<string>.Ok(htmlBody);
            
        }
        private async Task<Result<string>> GenerateConfirmationLinkAsync(ApplicationUser user)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            var scheme = request?.Scheme ?? "https";
            var host = request?.Host.ToString() ?? "localhost";

            var token = await _emailRepository.GenerateEmailConfirmationTokenAsync(user);

            if (string.IsNullOrEmpty(token)) return Result<string>.Failure("Erro ao gerar token de confirmação de e-mail.");

            var emailConfirmationToken = new EmailConfirmationToken(user, token);

            var result = await _emailRepository.SaveTokenEmailConfirmationAsync(emailConfirmationToken);

            if (!result) return Result<string>.Failure("Erro ao salvar token de confirmação de e-mail.");

            var confirmationLink = $"{scheme}://{host}/api/auth/confirmationUserEmail?id={emailConfirmationToken.Id}";

            return Result<string>.Ok(confirmationLink);
        }

    }
}
