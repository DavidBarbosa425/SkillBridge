using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ConfirmEmailRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
