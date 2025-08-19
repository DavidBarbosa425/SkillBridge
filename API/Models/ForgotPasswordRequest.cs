using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
