using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IValidatorService _validatorService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IApplicationMapper _mapper;

        public CompanyService(
            IValidatorService validatorService,
            ICompanyRepository companyRepository,
            IApplicationMapper mapper)
        {
            _validatorService = validatorService;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<Result<Company>> RegisterAsync(RegisterCompanyDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var mapToCompany = _mapper.Company.ToCompany(dto);

            var registerCompanyResult = await _companyRepository.AddAsync(mapToCompany);

            if (!registerCompanyResult.Success)
                return Result<Company>.Failure(registerCompanyResult.Message);


            return Result<Company>.Ok(registerCompanyResult.Data);
        }
    }
}
