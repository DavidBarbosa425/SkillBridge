using API.Extensions;
using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentityConfiguration();
builder.Services.AddCustomConfigurations(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddApiServices();
builder.Services.AddJwtBearer(builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SeuProjeto API", Version = "v1" });

    // Adiciona suporte a JWT no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite 'Bearer {seu_token}' no campo abaixo.\n\nExemplo: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
});


builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

    options.AddPolicy("ProductionCorsPolicy", policy =>
    {
        policy.WithOrigins("https://skillbridge.com.br")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

await app.SeedRolesAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(); 
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkillBridge v1");
        c.RoutePrefix = string.Empty;
    });

    app.UseCors("DevelopmentCorsPolicy");

}
else
{
    app.UseCors("ProductionCorsPolicy");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Run();
