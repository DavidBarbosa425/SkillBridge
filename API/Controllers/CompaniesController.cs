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
    [Authorize(Roles = Roles.User)]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IApiMapper _apiMapper;

        public CompaniesController(
            ICompanyService companyService,
            IApiMapper apiMapper)
        {
            _companyService = companyService;
            _apiMapper = apiMapper;
        }

        //[HttpGet("claims")]
        //public IActionResult GetClaims()
        //{
        //    return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
        //}

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCompanyRequest request)
        {
            var dto = _apiMapper.Company.ToRegisterCompanyDto(request);

            var result = await _companyService.RegisterAsync(dto);

            return this.ToActionResult(result);
        }
    }
}
