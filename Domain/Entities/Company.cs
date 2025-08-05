namespace Domain.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public Guid DomainUserId { get; set; }
        public User? DomainUser { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
    }
}
