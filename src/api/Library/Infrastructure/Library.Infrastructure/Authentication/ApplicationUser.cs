using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Authentication;

public class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}