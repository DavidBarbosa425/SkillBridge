using Microsoft.AspNetCore.Identity;
namespace Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser() { }
    public ApplicationUser(string name, string email)
    {
        UserName = name;
        Email = email;
    }
}
