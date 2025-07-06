using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Infrastructure.Services
{
    public class UrlService : IUrlService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GenerateApiUrl(string controller, string method, Dictionary<string, string?>? queryParams = null)
        {
            var request = _httpContextAccessor.HttpContext?.Request;

            var scheme = request?.Scheme ?? Uri.UriSchemeHttps;
            var host = request?.Host.Value ?? "localhost";

            var path = $"api/{controller}/{method}";
            var baseUrl = $"{scheme}://{host}/{path}";

            if (queryParams is not null && queryParams.Any())
            {
                baseUrl = QueryHelpers.AddQueryString(baseUrl, queryParams!);
            }

            return baseUrl;
        }
    }
}
