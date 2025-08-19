using Domain.Common;
using Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Extensions
{
    public static class ResultToActionResultExtensions
    {
        public static IActionResult ToActionResult(this ControllerBase ctrl, Result result)
        {
            if (result.Success) return ctrl.Ok(new { message = result.Message });

            return (result.Error) switch
            {
                ErrorType.Validation => ctrl.BadRequest(new { message = result.Message, errors = result.Errors }),
                ErrorType.Unauthorized => ctrl.Unauthorized(new { message = result.Message }),
                ErrorType.Forbidden => ctrl.StatusCode((int)HttpStatusCode.Forbidden, new { message = result.Message }),
                ErrorType.NotFound => ctrl.NotFound(new { message = result.Message }),
                ErrorType.Conflict => ctrl.Conflict(new { message = result.Message }),
                _ => ctrl.StatusCode((int)HttpStatusCode.InternalServerError, new { message = result.Message })
            };
        }

        public static IActionResult ToActionResult<T>(this ControllerBase ctrl, Result<T> result, Func<T, object>? projector = null)
        {
            if (result.Success)
                return ctrl.Ok(projector is null ? result.Data : projector(result.Data));

            return ctrl.ToActionResult((Result)result);
        }
    }
}