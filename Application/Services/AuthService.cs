

using Application.DTOs;
using Application.Interfaces;
using D.Common;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<string>> RegisterUserAsync(RegisterUserDto dto)
        {
            var creationResult = await _userRepository.AddAsync(dto.Name, dto.Email, dto.Password);

            if(!creationResult.Success) return Result<string>.Failure("Erro ao criar Usuário, tente novamente mais tarde");

            return Result<string>.Ok("Usuário criado com sucesso");

        }
    }
}
