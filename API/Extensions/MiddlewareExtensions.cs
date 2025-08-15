using API.Middlewares;

namespace API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }

        public static IApplicationBuilder UseHybridAuthenticationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<HybridAuthMiddleware>();

            return app;
        }

    }
}