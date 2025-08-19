using API.Interfaces.Mappers;

namespace API.Mappers
{
    public class ApiMapper : IApiMapper
    {
        public IApiCompanyMapper Company { get; }
        public IApiUserMapper User { get; }

        public ApiMapper(
            IApiUserMapper userMapper,
            IApiCompanyMapper companyMapper)
        {
            User = userMapper;
            Company = companyMapper;
        }
    }
}
