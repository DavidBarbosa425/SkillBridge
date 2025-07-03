using Infrastructure.Configurations;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
            services.Configure<EmailSettings>(configuration.GetSection("Email"));
            return services;
        }
    }
}
