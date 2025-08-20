using Domain.Interfaces;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class UrlService : IUrlService
    {
        private readonly UrlSettings _urlOptions;

        public UrlService(
            IOptions<UrlSettings> urlOptions)
        {
            _urlOptions = urlOptions.Value;
        }
        public string GenerateApiUrlEmailConfirmation(Guid userId, string token)
        {
            var url = string.Format(_urlOptions.AccountEmailConfirmation, userId, Uri.EscapeDataString(token));

            return url;
        }
        public string GenerateUrlEmailPasswordReset(Guid userId, string token)
        {
            var url = string.Format(_urlOptions.AccountEmailPasswordReset, userId, Uri.EscapeDataString(token));

            return url;
        }
    }
}
