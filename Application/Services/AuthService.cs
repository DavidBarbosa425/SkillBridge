

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
        public string RegisterUserAsync(RegisterUserDto dto)
        {
            _userRepository.AddAsync();
            return "resgidtrado";
        }
    }
}
