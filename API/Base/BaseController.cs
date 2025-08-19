using API.Extensions;
using API.Interfaces.Mappers;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected readonly IApiMapper _apiMapper;

        protected BaseController(IApiMapper apiMapper)
        {
            _apiMapper = apiMapper;
        }
        protected IActionResult ReturnResult(Result result) 
            => this.ToActionResult(result);

        protected IActionResult ReturnResult<T>(Result<T> result, Func<T, object>? projector = null)
            => this.ToActionResult(result, projector);

        protected Guid? GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return claim is null ? null : Guid.Parse(claim);
        }
        protected string? GetUserEmail()
        {
            return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }
        protected IEnumerable<string> GetUserRoles()
        {
            return User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
        }
    }
}
