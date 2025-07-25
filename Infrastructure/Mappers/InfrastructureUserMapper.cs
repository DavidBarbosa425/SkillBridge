using Domain.Entities;
using Infrastructure.Identity.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Mappers
{
    public class InfrastructureUserMapper : IInfrastructureUserMapper
    {
        public ApplicationUser ToApplicationUser(User user)
        {
            return new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(), 
                UserName = user.Email,
                Email = user.Email
            };

        }

        public User ToUser(ApplicationUser applicationUser)
        {
            return new User
            {
                Name = applicationUser.UserName!,
                Email = applicationUser.Email!,
                IdentityUserId = applicationUser.Id
            };
        }

    }
}
