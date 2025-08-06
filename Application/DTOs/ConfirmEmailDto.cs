namespace Application.DTOs
{
    public class ConfirmEmailDto
    {
        public Guid IdentityId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
