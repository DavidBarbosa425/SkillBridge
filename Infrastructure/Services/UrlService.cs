using Domain.Interfaces;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class UrlService : IUrlService
    {
        private readonly EmailConfirmationSettings _emailConfirmationOptions;

        public UrlService(
            IOptions<EmailConfirmationSettings> emailConfirmationOptions)
        {
            _emailConfirmationOptions = emailConfirmationOptions.Value;
        }
        public string GenerateApiUrlEmailConfirmation(string userId, string token)
        {
            var url = string.Format(_emailConfirmationOptions.Url, userId, Uri.EscapeDataString(token));

            return url;
        }
    }
}
