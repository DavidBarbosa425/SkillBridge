namespace Domain.Entities
{
    public class UserRegistered
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string IdentityId { get; set; } = string.Empty;
    }
}
