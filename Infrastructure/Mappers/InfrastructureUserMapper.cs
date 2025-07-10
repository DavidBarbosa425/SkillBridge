using Domain.Entities;
using Infrastructure.Identity.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Mappers
{
    internal class InfrastructureUserMapper : IInfrastructureUserMapper
    {
        public ApplicationUser ToApplicationUser(User user)
        {
            return new ApplicationUser
            {
                UserName = user.Email,
                Email = user.Email
            };
        }

    }
}
