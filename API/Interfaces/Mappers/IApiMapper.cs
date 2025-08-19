namespace API.Interfaces.Mappers
{
    public interface IApiMapper
    {
        IApiUserMapper User { get; }
        IApiCompanyMapper Company { get; }
    }
}
