using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService 
    {
        private readonly IValidatorsService _validatorService;

        public AuthService(
            IValidatorsService validatorService
            )
        {
            _validatorService = validatorService;
        }
     
        public async Task LoginAsync(LoginDto dto)
        {
            await _validatorService.ValidateAsync(dto);
        }
    }
}
