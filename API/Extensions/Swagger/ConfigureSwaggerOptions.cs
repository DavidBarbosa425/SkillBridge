using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Extensions.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            Console.WriteLine("=== SWAGGER DEBUG ===");
            Console.WriteLine($"Total de versões: {_provider.ApiVersionDescriptions.Count()}");

            foreach (var description in _provider.ApiVersionDescriptions)
            {
                Console.WriteLine($"Versão: {description.GroupName}");
                Console.WriteLine($"API Version: {description.ApiVersion}");
                Console.WriteLine($"Is Deprecated: {description.IsDeprecated}");
                Console.WriteLine("---");

                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = "SkillBridge API",
                    Version = description.ApiVersion.ToString(),
                    Description = $"Versão {description.ApiVersion} da API"
                });
            }

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Digite 'Bearer {seu_token}'"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        }
    }
}
