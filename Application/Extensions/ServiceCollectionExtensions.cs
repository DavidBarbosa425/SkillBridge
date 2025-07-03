using Application.Interfaces;
using Application.Services;
using Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IValidatorService, ValidatorService>();

            return services;
        }
    }
}
