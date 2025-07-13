namespace Domain.Entities
{
    public class EmailConfirmationToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; } = DateTime.UtcNow.AddHours(1);

        public EmailConfirmationToken() { }

    }
}
