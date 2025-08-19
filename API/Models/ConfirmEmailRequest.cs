using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ConfirmEmailRequest
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
