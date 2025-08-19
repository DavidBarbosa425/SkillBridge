namespace Application.DTOs
{
    public class LoginResultDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public UserDto User { get; set; } = null!;
        public int ExpiresIn { get; set; }
    }
}
