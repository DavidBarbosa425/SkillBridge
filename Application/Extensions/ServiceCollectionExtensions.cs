using Application.Factories;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Interfaces.Mappers;
using Application.Mappers;
using Application.Services;
using Application.Services.Emails;
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
            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IValidatorService, ValidatorService>();
            services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();

            // Validators
            services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();

            // Mappers
            services.AddScoped<IApplicationMapper, ApplicationMapper>();
            services.AddScoped<IApplicationUserMapper, ApplicationUserMapper>();

            // Factories
            services.AddScoped<IEmailTemplateFactory, EmailTemplateFactory>();

            return services;
        }
    }
}
