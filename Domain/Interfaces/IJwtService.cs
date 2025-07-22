using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateToken(User user);
    }
}
