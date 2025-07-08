using Application.Interfaces;
using Application.Interfaces.Mappers;
using Application.Mappers;
using Application.Services.Auth;
using Application.Validators;
using Application.Validators.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IValidatorService, ValidatorService>();

            // Validators
            services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();

            // Mappers
            services.AddScoped<IApplicationMapper, ApplicationMapper>();
            services.AddScoped<IApplicationUserMapper, ApplicationUserMapper>();
            services.AddScoped<IApplicationEmailMapper, ApplicationEmailMapper>();

            return services;
        }
    }
}
