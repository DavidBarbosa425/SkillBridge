

using Application.DTOs;
using Application.Interfaces;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public AuthService(
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public IEmailService EmailService { get; }

        public async Task<Result<string>> RegisterUserAsync(RegisterUserDto dto)
        {
            var creationResult = await _userRepository.AddAsync(dto.Name, dto.Email, dto.Password);

            if(!creationResult.Success) return Result<string>.Failure("Erro ao criar Usuário, tente novamente mais tarde");

            _emailService.SendRegistrationConfirmationAsync(dto.Name, dto.Email);

            return Result<string>.Ok("Usuário criado com sucesso");

        }
    }
}
