namespace Application.DTOs
{
    public class RegisterItServiceProviderDto
    {
        public Guid UserId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
    }
}
