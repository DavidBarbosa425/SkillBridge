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
            if (result.Success) return ctrl.Ok(Result.Ok(result.Message));
            
            return (result.Error) switch
            {
                ErrorType.Validation => ctrl.BadRequest(Result.Validation(result.Errors)), // 400
                ErrorType.Unauthorized => ctrl.Unauthorized(Result.Failure(result.Message)), // 401
                ErrorType.Forbidden => ctrl.StatusCode((int)HttpStatusCode.Forbidden, Result.Failure(result.Message)), // 403
                ErrorType.NotFound => ctrl.NotFound(Result.Failure(result.Message)), // 404
                ErrorType.Conflict => ctrl.Conflict(Result.Failure(result.Message)), // 409
                _ => ctrl.StatusCode((int)HttpStatusCode.InternalServerError, Result.Failure(result.Message)) // 500
            };
        }

        public static IActionResult ToActionResult<T>(this ControllerBase ctrl, Result<T> result, Func<T, object>? projector = null)
        {
            if (result.Success)
                return ctrl.Ok(new
                {
                    success = true,
                    message = result.Message,
                    data = projector is null ? result.Data : projector(result.Data)
                });

            return ctrl.ToActionResult((Result)result);
        }
    }
}