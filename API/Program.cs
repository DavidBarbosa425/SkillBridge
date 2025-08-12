using API.Extensions;
using API.Extensions.Swagger;
using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddLogging();

// API Configuration
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApiServices();
builder.Services.AddIdentityConfiguration();
builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddCustomConfigurations(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();


// Application Configuration
builder.Services.AddApplicationServices();

// Infrastructure Configuration
builder.Services.AddInfrastructureServices();
builder.Services.AddMassTransitWithRabbitMq(builder.Configuration);

builder.Services.AddOpenApi();

builder.Services.AddProjectCors();

var app = builder.Build();

await app.SeedRolesAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerDocumentation();

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
