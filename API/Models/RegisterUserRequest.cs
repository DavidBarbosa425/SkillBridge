using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RegisterUserRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string PreferredName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
