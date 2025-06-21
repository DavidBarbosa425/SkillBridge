using Microsoft.AspNetCore.Identity;
namespace Domain.Entities
{

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() {}
        public ApplicationUser(string name, string email)
        {
            UserName = name;
            Email = email;
        }
    }
}
