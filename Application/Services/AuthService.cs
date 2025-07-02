using Application.DTOs;
using Application.Interfaces;
using Application.Templates;
using Domain.Common;
using Domain.Constants;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services

{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IEmailRepository _emailRepository;

        public AuthService(
            IUserRepository userRepository,
            IEmailService emailService,
            IEmailRepository emailRepository)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _emailRepository = emailRepository;
        }

        public async Task<Result<string>> RegisterUserAsync(RegisterUserDto dto)
        {
            var creationResult = await _userRepository.AddAsync(dto.Name, dto.Email, dto.Password);

            if(!creationResult.Success) return Result<string>.Failure("Erro ao criar Usuário, tente novamente mais tarde");

            var result = await SendEmailConfirmationAsync(dto.Name, dto.Email);

            if(!result.Success) return Result<string>.Failure(result.Message);

            return Result<string>.Ok("Usuário criado com sucesso. Um E-mail de Confirmação foi enviado para sua caixa de entrada.");

        }

        private async Task<Result<string>> SendEmailConfirmationAsync(string name, string email)
        {
            var resultEmailBody = await GenerateEmailConfirmationAsync(name, email);

            if (!resultEmailBody.Success) return Result<string>.Failure(resultEmailBody.Message);

            await _emailService.SendEmailAsync(email, EmailSubjects.Confirmation, resultEmailBody.Data);

            return Result<string>.Ok("E-mail enviado com sucesso!");
        }

        private async Task<Result<string>> GenerateEmailConfirmationAsync(string name, string email)
        {
            var confirmationLinkResult = await GenerateLinkConfirmationAsync(name, email);

            if (!confirmationLinkResult.Success) return Result<string>.Failure(confirmationLinkResult.Message);

            var htmlBody = EmailTemplateFactory.GenerateConfirmationEmailHtml(name, confirmationLinkResult.Data);

            return Result<string>.Ok(htmlBody);
            
        }
        private async Task<Result<string>> GenerateLinkConfirmationAsync(string name, string email)
        {

            var token = await _emailRepository.GenerateEmailConfirmationTokenAsync(email);

            if (string.IsNullOrEmpty(token)) return Result<string>.Failure("Erro ao gerar token de confirmação de e-mail.");

            var result = await _emailRepository.SaveTokenEmailConfirmationAsync(email, token);

            if (!result) return Result<string>.Failure("Erro ao salvar token de confirmação de e-mail.");

            var idToken = await _emailRepository.GetTokenEmailConfirmationIdAsync(email);

            if (idToken == null) return Result<string>.Failure("Erro ao buscar token de confirmação de e-mail.");

            var confirmationLink = _emailService.GenerateLinkEndPoint("auth", "confirmationUserEmail", idToken.ToString());

            return Result<string>.Ok(confirmationLink);
        }

    }
}
