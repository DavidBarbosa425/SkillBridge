namespace API.Models
{
    public class ConfirmEmailRequest
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
