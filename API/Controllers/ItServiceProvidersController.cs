using API.Base;
using API.Interfaces.Mappers;
using API.Models;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ItServiceProvidersController : BaseController
    {
        private readonly IItServiceProviderService _itServiceProviderService;

        public ItServiceProvidersController(
            IItServiceProviderService itServiceProviderService,
            IApiMapper apiMapper)
             : base(apiMapper)
        {
            _itServiceProviderService = itServiceProviderService;
        }

        [HttpPost("register")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Register([FromBody] RegisterItServiceProviderRequest request)
        {
            var dto = _apiMapper.ItServiceProvider.ToRegisterItServiceProviderDto(request);

            var result = await _itServiceProviderService.RegisterAsync(dto);

            return ReturnResult(result);
        }
    }
}
