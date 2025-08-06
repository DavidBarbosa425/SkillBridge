using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Constants;
using Domain.Interfaces;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IValidatorService _validatorService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IApplicationMapper _mapper;
        private readonly IIdentityUserService _identityUserService;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(
            IValidatorService validatorService,
            ICompanyRepository companyRepository,
            IApplicationMapper mapper,
            IIdentityUserService identityUserService,
            IUnitOfWork unitOfWork)
        {
            _validatorService = validatorService;
            _companyRepository = companyRepository;
            _mapper = mapper;
            _identityUserService = identityUserService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> RegisterAsync(RegisterCompanyDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var mapToCompany = _mapper.Company.ToCompany(dto);

            await _unitOfWork.BeginTransactionAsync();

            var registerCompanyResult = await _companyRepository.AddAsync(mapToCompany);

            if (!registerCompanyResult.Success)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure(registerCompanyResult.Message);
            }

            var roleAssignedResult = await _identityUserService.AssignRoleAsync(registerCompanyResult.Data.User.IdentityId, Roles.Company);

            if (!roleAssignedResult.Success)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure(roleAssignedResult.Message);
            }

            await _unitOfWork.CommitAsync();

            return Result.Ok(registerCompanyResult.Message);
        }
    }
}
