using Domain.Interfaces;
using Infrastructure.Configurations;
using Infrastructure.Identity;
using Infrastructure.Interfaces;
using Infrastructure.Mappers;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do RabbitMQ
            var rabbitMqSettings = configuration.GetSection("RabbitMq").Get<RabbitMqSettings>();

            services.AddMassTransit(x =>
            {
                // Configurar consumers
                x.AddConsumer<EmailConsumer>();
                x.AddConsumer<UserRegisteredConsumer>();
                x.AddConsumer<UserForgotPasswordConsumer>();

                // Configurar RabbitMQ
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqSettings.Host, rabbitMqSettings.VirtualHost, h =>
                    {
                        h.Username(rabbitMqSettings.Username);
                        h.Password(rabbitMqSettings.Password);
                    });

                    // Configurar endpoints para consumers
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            // Services
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMessageBrokerService, RabbitMqService>();

            // Mappers
            services.AddScoped<IInfrastructureMapper, InfrastructureMapper>();
            services.AddScoped<IInfrastructureUserMapper, InfrastructureUserMapper>();

            //repositories
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
