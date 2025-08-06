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
        public string GenerateApiUrlEmailConfirmation(string identityId, string token)
        {
            var url = string.Format(_urlOptions.AccountEmailConfirmation, identityId, Uri.EscapeDataString(token));

            return url;
        }
        public string GenerateUrlEmailPasswordReset(string identityId, string token)
        {
            var url = string.Format(_urlOptions.AccountEmailPasswordReset, identityId, Uri.EscapeDataString(token));

            return url;
        }
    }
}
