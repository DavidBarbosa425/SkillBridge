using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool? RememberMe { get; set; }
    }
}
