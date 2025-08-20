using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUrlService
    {
        string GenerateApiUrlEmailConfirmation(Guid userId, string token);
        string GenerateUrlEmailPasswordReset(Guid userId, string token);
    }
}
