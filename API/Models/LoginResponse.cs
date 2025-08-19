using Application.DTOs;

namespace API.Models
{
    public class LoginResponse
    {
        public UserDto User { get; set; } = null!;
        public long ExpiresIn { get; set; }
    }
}
