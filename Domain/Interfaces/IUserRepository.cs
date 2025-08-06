using Domain.Common;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<User>> AddAsync(User user);
        Task<Result<User>> FindByIdAsync(string id);
        Task<Result<User>> FindByEmailAsync(string email);
    }
}
