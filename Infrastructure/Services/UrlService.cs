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
        public string GenerateApiUrlEmailConfirmation(Guid identityId, string token)
        {
            var url = string.Format(_urlOptions.AccountEmailConfirmation, identityId, Uri.EscapeDataString(token));

            return url;
        }
        public string GenerateUrlEmailPasswordReset(Guid identityId, string token)
        {
            var url = string.Format(_urlOptions.AccountEmailPasswordReset, identityId, Uri.EscapeDataString(token));

            return url;
        }
    }
}
