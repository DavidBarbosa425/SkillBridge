

using Application.DTOs;
using Application.Interfaces;
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
        public async Task<string> RegisterUserAsync(RegisterUserDto dto)
        {
            var creationResult = await _userRepository.AddAsync(dto.Name, dto.Email, dto.Password);

            return "teste";
        }
    }
}
