namespace Application.Interfaces.Mappers
{
    public interface IApplicationMapper
    {
        IApplicationUserMapper User { get; }
        IApplicationEmailMapper Email { get; }
    }
}
