using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Library.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Rentals;
using Library.Domain.Staff;
using Library.Infrastructure.Domain.Rentals;
using Library.Infrastructure.Domain.Staff;

namespace Library.Infrastructure.Data;

public class LibraryContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public virtual DbSet<Rental> Rentals { get; set; }
    
    public virtual DbSet<BookRental> BookRentals { get; set; }
    
    public virtual DbSet<Employee> Employees { get; set; }
    
    
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new BookRentalEntityConfiguration());
        builder.ApplyConfiguration(new RentalEntityConfiguration());
        builder.ApplyConfiguration(new EmployeeEntityConfiguration());
    }
}