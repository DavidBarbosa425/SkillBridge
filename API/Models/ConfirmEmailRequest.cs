namespace API.Models
{
    public class ConfirmEmailRequest
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
