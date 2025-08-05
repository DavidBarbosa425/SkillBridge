namespace Domain.Entities
{
    public class ItServiceProvider
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string JobTitle { get; set; } = string.Empty;
    }
}
