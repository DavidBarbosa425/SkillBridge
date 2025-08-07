using Application.DTOs;
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

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterItServiceProviderDto dto)
        {
            var result = await _companyService.RegisterAsync(dto);

            return Ok(result);
        }
    }
}
