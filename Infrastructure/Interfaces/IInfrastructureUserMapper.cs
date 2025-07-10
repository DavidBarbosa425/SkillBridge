using Domain.Entities;
using Infrastructure.Identity.Models;

namespace Infrastructure.Interfaces
{
    public interface IInfrastructureUserMapper
    {
        ApplicationUser ToApplicationUser(User user);
    }
}
