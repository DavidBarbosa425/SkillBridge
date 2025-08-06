namespace Domain.Interfaces
{
    public interface IUrlService
    {
        string GenerateApiUrlEmailConfirmation(string identityId, string token);
        string GenerateUrlEmailPasswordReset(string identityId, string token);
    }
}
