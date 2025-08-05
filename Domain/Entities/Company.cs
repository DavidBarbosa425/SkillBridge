namespace Domain.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
    }
}
