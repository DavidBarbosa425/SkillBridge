using Application.DTOs;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Roles.User)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        //[HttpGet("claims")]
        //public IActionResult GetClaims()
        //{
        //    return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
        //}

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCompanyDto dto)
        {
            var result = await _companyService.RegisterAsync(dto);

            return Ok(result);
        }
    }
}
