namespace Domain.Interfaces
{
    public interface IUrlService
    {
        string GenerateApiUrlEmailConfirmation(string userId, string token);
    }
}
