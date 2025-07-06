namespace Domain.Interfaces
{
    public interface IUrlService
    {
        string GenerateApiUrl(string controller, string method, Dictionary<string, string?>? Idparam);
    }
}
