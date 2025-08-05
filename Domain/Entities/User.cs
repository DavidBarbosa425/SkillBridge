using Domain.Constants;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PreferredName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string IdentityUserId { get; set; } = string.Empty;
        public ICollection<ItServiceProvider>? ItServiceProviders { get; set; } 
        public ICollection<Company>? Companies { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } 
        public bool IsActive { get; set; } = true;

    }
}
