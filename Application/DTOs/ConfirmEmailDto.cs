namespace Application.DTOs
{
    public class ConfirmEmailDto
    {
        public string IdentityId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
