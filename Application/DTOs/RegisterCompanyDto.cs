namespace Application.DTOs
{
    public class RegisterCompanyDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
    }
}
