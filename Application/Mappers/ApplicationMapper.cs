using Application.Interfaces.Mappers;

namespace Application.Mappers
{
    public class ApplicationMapper : IApplicationMapper
    {
        public IApplicationUserMapper User { get; }

        public ApplicationMapper(
            IApplicationUserMapper userMapper)
        {
            User = userMapper;
        }
    }

}
