using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IJwtService
    {
        TokenResult GenerateToken(User user,IList<string> roles);
    }
}
