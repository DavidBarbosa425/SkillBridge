namespace Domain.Entities
{
    public class UserForgotPassword
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid IdentityId { get; set; }
    }
}
