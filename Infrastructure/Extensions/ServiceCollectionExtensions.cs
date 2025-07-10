using Domain.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Mappers;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();

            // Services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUrlService, UrlService>();

            // Mappers
            services.AddScoped<IInfrastructureMapper, InfrastructureMapper>();
            services.AddScoped<IInfrastructureUserMapper, InfrastructureUserMapper>();

            return services;
        }
    }
}
