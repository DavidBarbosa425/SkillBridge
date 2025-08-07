namespace Application.Interfaces.Mappers
{
    public interface IApplicationMapper
    {
        IApplicationUserMapper User { get; }
        IApplicationCompanyMapper Company { get; set; }
        IApplicationItServiceProviderMapper ItServiceProvider { get; set; }
    }
}
