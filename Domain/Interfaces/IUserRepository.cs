using Domain.Common;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<string>> AddAsync(string name, string email, string password);
    }
}
