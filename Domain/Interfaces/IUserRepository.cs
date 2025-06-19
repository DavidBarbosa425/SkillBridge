using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        void AddAsync(ApplicationUser applicationUser);
    }
}
