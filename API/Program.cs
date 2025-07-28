using API.Extensions;
using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddLogging();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApiServices();
builder.Services.AddIdentityConfiguration();
builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddCustomConfigurations(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddOpenApi();

builder.Services.AddProjectCors();

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
