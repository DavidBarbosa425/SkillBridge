using Domain.Entities;
using Infrastructure.Identity.Models;

namespace Infrastructure.Mappers
{
    public static class InfrastructureUserMapper
    {
        public static ApplicationUser ToApplicationUser(User user)
        {
            return new ApplicationUser
            {
                UserName = user.Email,
                Email = user.Email
            };
        }

    }
}
