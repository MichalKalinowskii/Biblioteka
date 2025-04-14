using Library.Domain.Clients;
using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Authentication;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public virtual Client Client { get; set; }
}