using Application.Interfaces.Mappers;

namespace Application.Mappers
{
    public class ApplicationMapper : IApplicationMapper
    {
        public IApplicationUserMapper User { get; }
        public IApplicationEmailMapper Email { get; }

        public ApplicationMapper(
            IApplicationUserMapper userMapper,
            IApplicationEmailMapper emailMapper)
        {
            User = userMapper;
            Email = emailMapper;
        }
    }

}
