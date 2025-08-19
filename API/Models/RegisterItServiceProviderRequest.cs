using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RegisterItServiceProviderRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string JobTitle { get; set; } = string.Empty;
    }
}
