namespace Domain.Interfaces
{
    public interface IUrlService
    {
        string GenerateApiUrlEmailConfirmation(Guid identityId, string token);
        string GenerateUrlEmailPasswordReset(Guid identityId, string token);
    }
}
