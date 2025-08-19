using API.Interfaces.Mappers;

namespace API.Mappers
{
    public class ApiMapper : IApiMapper
    {
        public IApiCompanyMapper Company { get; }
        public IApiUserMapper User { get; }
        public IApiItServiceProviderMapper ItServiceProvider { get; }

        public ApiMapper(
            IApiUserMapper userMapper,
            IApiCompanyMapper companyMapper,
            IApiItServiceProviderMapper itServiceProvider)
        {
            User = userMapper;
            Company = companyMapper;
            ItServiceProvider = itServiceProvider;
        }
    }
}
