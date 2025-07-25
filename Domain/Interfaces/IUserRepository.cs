using Domain.Common;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Result<User> AddAsync(User user);
    }
}
