using Domain.Interfaces;
using Infrastructure.Identity;
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
            // Services
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IJwtService, JwtService>();

            // Mappers
            services.AddScoped<IInfrastructureMapper, InfrastructureMapper>();
            services.AddScoped<IInfrastructureUserMapper, InfrastructureUserMapper>();

            //repositories
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
