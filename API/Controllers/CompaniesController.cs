using API.Base;
using API.Interfaces.Mappers;
using API.Models;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CompaniesController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(
            ICompanyService companyService,
            IApiMapper apiMapper,
            ILogger<BaseController> logger)
             : base(apiMapper, logger)
        {
            _companyService = companyService;
        }

        [HttpPost("register")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Register([FromBody] RegisterCompanyRequest request)
        {
            var dto = _apiMapper.Company.ToRegisterCompanyDto(request);

            var result = await _companyService.RegisterAsync(dto);

            return ReturnResult(result);
        }
    }
}
