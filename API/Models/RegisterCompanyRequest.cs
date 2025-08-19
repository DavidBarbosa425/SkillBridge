using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RegisterCompanyRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string CNPJ { get; set; } = string.Empty;
    }
}
