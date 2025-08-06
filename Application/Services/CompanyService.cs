using Application.DTOs;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IValidatorService _validatorService;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(
            IValidatorService validatorService,
            ICompanyRepository companyRepository)
        {
            _validatorService = validatorService;
            _companyRepository = companyRepository;
        }
        public async Task<Result<Company>> RegisterAsync(RegisterCompanyDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var registerCompanyResult = await _companyRepository.AddAsync(new Domain.Entities.Company());

            if (!registerCompanyResult.Success)
                return Result<Company>.Failure(registerCompanyResult.Message);


            return Result<Company>.Ok(registerCompanyResult.Data);
        }
    }
}
