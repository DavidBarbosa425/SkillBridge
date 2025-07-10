using Infrastructure.Interfaces;

namespace Infrastructure.Mappers
{
    public class InfrastructureMapper : IInfrastructureMapper
    {
        public IInfrastructureUserMapper User { get;}
        public InfrastructureMapper(IInfrastructureUserMapper userMapper)
        {
            User = userMapper;
        }
    }

}
