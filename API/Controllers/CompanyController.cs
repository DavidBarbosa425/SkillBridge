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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
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
