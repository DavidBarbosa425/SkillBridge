using Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Middlewares
{
    public class HybridAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HybridAuthMiddleware> _logger;

        public HybridAuthMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<HybridAuthMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("🔍 Middleware executando para: {Method} {Path}", context.Request.Method, context.Request.Path);

            var token = ExtractToken(context);

            if (!string.IsNullOrEmpty(token))
            {
                var principal = ValidateToken(token);
                if (principal != null)
                {
                    context.User = principal;
                    _logger.LogInformation("✅ Usuário autenticado: {UserId}", principal.Identity?.Name);
                }
            }
            else
            {
                _logger.LogWarning("⚠️ Nenhum token encontrado");
            }

            await _next(context);
        }

        private string? ExtractToken(HttpContext context)
        {

            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            _logger.LogInformation("🔍 Authorization header: {Header}", authHeader ?? "NULL");

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var bearerToken = authHeader.Substring("Bearer ".Length);
                _logger.LogInformation("✅ Bearer token extraído: {TokenLength} caracteres", bearerToken.Length);
                return bearerToken;
            }

            _logger.LogInformation("🍪 Todos os cookies recebidos:");

            foreach (var cookie in context.Request.Cookies)
            {
                _logger.LogInformation("  - {Name}: {Value}", cookie.Key, cookie.Value);
            }

            var cookieToken = context.Request.Cookies["auth_token"];

            _logger.LogInformation("�� Cookie auth_token: {HasCookie}", !string.IsNullOrEmpty(cookieToken));

            if (!string.IsNullOrEmpty(cookieToken))
            {
                _logger.LogInformation("✅ Cookie token extraído: {TokenLength} caracteres", cookieToken.Length);

                return cookieToken;
            }

            // 3. Verificar se o usuário já está autenticado
            _logger.LogInformation("�� Usuário atual: {IsAuthenticated}", context.User?.Identity?.IsAuthenticated ?? false);

            return null;
        }

        private ClaimsPrincipal? ValidateToken(string token)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("Jwt").Get<JwtSettings>();
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(jwtSettings!.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    RoleClaimType = ClaimTypes.Role
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return principal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Erro ao validar token");
                return null;
            }
        }
    }
}