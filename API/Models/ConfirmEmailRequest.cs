namespace API.Models
{
    public class ConfirmEmailRequest
    {
        public string Id { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
