using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ResetPasswordDto
    {
        public string userId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
