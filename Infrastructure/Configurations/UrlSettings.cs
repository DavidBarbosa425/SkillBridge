using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Configurations
{
    public class UrlSettings
    {
        [Required]
        public string AccountEmailConfirmation { get; set; } = string.Empty;

        [Required]
        public string AccountEmailPasswordReset { get; set; } = string.Empty;
    }
}
