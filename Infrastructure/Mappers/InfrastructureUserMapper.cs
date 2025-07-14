using Domain.Entities;
using Infrastructure.Identity.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Mappers
{
    public class InfrastructureUserMapper : IInfrastructureUserMapper
    {
        public ApplicationUser ToApplicationUser(User user)
        {
            if(user.Id == Guid.Empty)
            {
                return new ApplicationUser
                {
                    UserName = user.Name,
                    Email = user.Email
                };
            }
            else
            {
                return new ApplicationUser
                {
                    Id = user.Id.ToString().ToUpper(),
                    UserName = user.Name,
                    Email = user.Email
                };
            }
        }

        public User ToUser(ApplicationUser applicationUser)
        {
            return new User
            {
                Id = Guid.Parse(applicationUser.Id),
                Name = applicationUser.UserName!,
                Email = applicationUser.Email!
            };
        }

    }
}
