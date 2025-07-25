using Domain.Common;
using Domain.Interfaces;
using System.Net;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        private readonly IServiceProvider _serviceProvider;

        public ExceptionMiddleware(
            RequestDelegate next, 
            ILogger<ExceptionMiddleware> logger, 
            IHostEnvironment env,
            IServiceProvider serviceProvider)
        {
            _next = next;
            _logger = logger;
            _env = env;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var unitOfWork = context.RequestServices.GetService<IUnitOfWork>();
                if (unitOfWork != null && unitOfWork.HasActiveTransaction)
                    await unitOfWork.RollbackAsync();// Rollback any transaction if an exception occurs

                _logger.LogError(ex, "Ocorreu uma exceção não tratada.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                string userMessage = "Ocorreu um erro inesperado. Tente novamente mais tarde.";

                if (_env.IsDevelopment())
                {
                    var resultDev = Result.Failure($"{ex.Message}\n{ex.StackTrace}");
                    await context.Response.WriteAsJsonAsync(resultDev);
                }
                else
                {
                    var resultProd = Result.Failure(userMessage);
                    await context.Response.WriteAsJsonAsync(resultProd);
                }
            }
        }
    }
}
