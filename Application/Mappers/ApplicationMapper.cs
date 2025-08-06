using Application.Interfaces.Mappers;

namespace Application.Mappers
{
    internal class ApplicationMapper : IApplicationMapper
    {
        public IApplicationUserMapper User { get; }
        public IApplicationCompanyMapper Company { get; set; }

        public ApplicationMapper(
            IApplicationUserMapper userMapper,
            IApplicationCompanyMapper companyMapper)
        {
            User = userMapper;
            Company = companyMapper;
        }
    }

}
