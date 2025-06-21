using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<string> AddAsync(string name, string email, string password);
    }
}
