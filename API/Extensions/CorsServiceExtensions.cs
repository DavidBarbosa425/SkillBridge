namespace API.Extensions
{
    public static class CorsServiceExtensions
    {
        public static IServiceCollection AddProjectCors(this IServiceCollection services, IConfiguration configuration)
        {
            var devOrigins = configuration.GetSection("Cors:Development").Get<string[]>();
            var prodOrigins = configuration.GetSection("Cors:Production").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddPolicy("DevelopmentCorsPolicy", policy =>
                {
                    policy.WithOrigins(devOrigins!)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });

                options.AddPolicy("ProductionCorsPolicy", policy =>
                {
                    policy.WithOrigins(prodOrigins!)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            return services;
        }
    }
}
