using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUrlService, UrlService>();

            return services;
        }
    }
}
