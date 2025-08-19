using API.Extensions;
using API.Interfaces.Mappers;
using API.Models;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItServiceProvidersController : ControllerBase
    {
        private readonly IItServiceProviderService _itServiceProviderService;
        private readonly IApiMapper _apiMapper;

        public ItServiceProvidersController(
            IItServiceProviderService itServiceProviderService,
            IApiMapper apiMapper)
        {
            _itServiceProviderService = itServiceProviderService;
            _apiMapper = apiMapper;
        }

        [HttpPost("register")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Register([FromBody] RegisterItServiceProviderRequest request)
        {
            var dto = _apiMapper.ItServiceProvider.ToRegisterItServiceProviderDto(request);

            var result = await _itServiceProviderService.RegisterAsync(dto);

            return this.ToActionResult(result);
        }
    }
}
