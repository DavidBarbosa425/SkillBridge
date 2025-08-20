namespace Application.DTOs
{
    public class ConfirmEmailDto
    {
        public Guid userId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
