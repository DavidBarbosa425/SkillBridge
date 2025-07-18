using API.Interfaces.Mappers;

namespace API.Mappers
{
    public class ApiMapper : IApiMapper
    {
        public IApiUserMapper User { get; }
        public ApiMapper(
            IApiUserMapper userMapper)
        {
            User = userMapper;
        }
    }
}
