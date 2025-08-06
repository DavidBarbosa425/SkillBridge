using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IValidatorService _validatorService;

        public CompanyService(IValidatorService validatorService)
        {
            _validatorService = validatorService;
        }
        public async Task<bool> RegisterAsync(RegisterCompanyDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            throw new NotImplementedException();
        }
    }
}
