namespace Domain.Entities
{
    public class ItServiceProvider
    {
        public Guid Id { get; set; }
        public Guid DomainUserId { get; set; }
        public User DomainUser { get; set; } = null!;
        public string JobTitle { get; set; } = string.Empty;
    }
}
