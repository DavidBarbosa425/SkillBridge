using Infrastructure.Interfaces;

namespace Infrastructure.Mappers
{
    internal class InfrastructureMapper : IInfrastructureMapper
    {
        public IInfrastructureUserMapper User { get;}
        public InfrastructureMapper(IInfrastructureUserMapper userMapper)
        {
            User = userMapper;
        }
    }

}
