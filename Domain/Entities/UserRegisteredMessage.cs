namespace Domain.Entities
{
    public class UserRegisteredMessage
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
