namespace Domain.Entities
{
    public class TokenResult
    {
        public string Token { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }
}
