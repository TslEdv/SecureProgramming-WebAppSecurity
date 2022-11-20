using Microsoft.AspNetCore.Identity;

namespace WebApp.Domain.Identity;

public class AppUser : IdentityUser
{
    public ICollection<Cesar>? Cesars { get; set; }
    public ICollection<DH>? DHs { get; set; }

}