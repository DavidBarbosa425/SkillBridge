using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Mappers;
using Domain.Common;
using Domain.Constants;
using Domain.Interfaces;

namespace Application.Services
{
    public class ItServiceProviderService : IItServiceProviderService
    {
        private readonly IApplicationMapper _mapper;
        private readonly IValidatorService _validatorService;
        private readonly IItServiceProviderRepository _itServiceProviderRepository;
        private readonly IIdentityUserService _identityUserService;
        private readonly IUnitOfWork _unitOfWork;

        public ItServiceProviderService(
            IApplicationMapper applicationMapper,
            IValidatorService validatorService,
            IItServiceProviderRepository itServiceProviderRepository,
            IIdentityUserService identityUserService,
            IUnitOfWork unitOfWork)
        {
            _mapper = applicationMapper;
            _validatorService = validatorService;
            _itServiceProviderRepository = itServiceProviderRepository;
            _identityUserService = identityUserService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> RegisterAsync(RegisterItServiceProviderDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var toItServiceProvider = _mapper.ItServiceProvider.ToItServiceProvider(dto);

            await _unitOfWork.BeginTransactionAsync();

            var addProviderResult = await _itServiceProviderRepository.AddAsync(toItServiceProvider);

            if (!addProviderResult.Success)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure(addProviderResult.Message);
            }

            var assignRoleResult = await _identityUserService.AssignRoleAsync(addProviderResult.Data.User.IdentityId, Roles.Developer);

            if (!assignRoleResult.Success)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Failure(assignRoleResult.Message);
            }
            
            await _unitOfWork.CommitAsync();

            return Result.Ok("It Service Provider registered successfully.");
        }
    }
}
