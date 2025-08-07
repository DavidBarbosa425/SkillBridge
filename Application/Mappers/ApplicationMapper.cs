using Application.Interfaces.Mappers;

namespace Application.Mappers
{
    internal class ApplicationMapper : IApplicationMapper
    {
        public IApplicationUserMapper User { get; }
        public IApplicationCompanyMapper Company { get; set; }
        public IApplicationItServiceProviderMapper ItServiceProvider { get; set; }

        public ApplicationMapper(
            IApplicationUserMapper userMapper,
            IApplicationCompanyMapper companyMapper,
            IApplicationItServiceProviderMapper itServiceProviderMapper)
        {
            User = userMapper;
            Company = companyMapper;
            ItServiceProvider = itServiceProviderMapper;
        }
    }

}
