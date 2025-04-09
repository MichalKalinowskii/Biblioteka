using Library.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data;

public class LibraryContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }
}