using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ResetPasswordRequest
    {
        [Required]
        public string userId { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;

    }
}
