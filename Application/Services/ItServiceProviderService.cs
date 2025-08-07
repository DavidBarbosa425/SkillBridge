using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Mappers;
using Domain.Common;

namespace Application.Services
{
    public class ItServiceProviderService : IItServiceProviderService
    {
        private readonly IApplicationMapper _mapper;
        private readonly IValidatorService _validatorService;

        public ItServiceProviderService(
            IApplicationMapper applicationMapper,
            IValidatorService validatorService )
        {
            _mapper = applicationMapper;
            _validatorService = validatorService;
        }
        public async Task<Result> RegisterAsync(RegisterItServiceProviderDto dto)
        {
            await _validatorService.ValidateAsync(dto);

            var toItServiceProvider = _mapper.ItServiceProvider.ToItServiceProvider(dto);

            throw new NotImplementedException();
        }
    }
}
