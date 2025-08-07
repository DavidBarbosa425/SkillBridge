using Application.DTOs;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Roles.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class ItServiceProvidersController : ControllerBase
    {
        private readonly IItServiceProviderService _itServiceProviderService;

        public ItServiceProvidersController(IItServiceProviderService itServiceProviderService)
        {
            _itServiceProviderService = itServiceProviderService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterItServiceProviderDto dto)
        {
            var result = await _itServiceProviderService.RegisterAsync(dto);

            return Ok(result);
        }
    }
}
