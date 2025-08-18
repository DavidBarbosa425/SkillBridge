using API.Extensions;
using API.Extensions.Swagger;
using Application.Extensions;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddLogging();

// API Configuration
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApiServices();
builder.Services.AddIdentityConfiguration();
//builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddCustomConfigurations(builder.Configuration);
builder.Services.AddConfigurationApiVersioning();
builder.Services.AddProjectCors();

builder.Services.AddSwaggerDocumentation();

// Application Configuration
builder.Services.AddApplicationServices();

// Infrastructure Configuration
builder.Services.AddInfrastructureServices();
builder.Services.AddMassTransitWithRabbitMq(builder.Configuration);

var app = builder.Build();

await app.SeedRolesAsync();

if (app.Environment.IsDevelopment())
{
    app.UseCors("DevelopmentCorsPolicy");

    app.UseSwaggerDocumentation();
}
else
{
    app.UseCors("ProductionCorsPolicy");
}

app.UseHttpsRedirection();

app.UseHybridAuthenticationMiddleware();
app.UseAuthorization();

app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Run();
