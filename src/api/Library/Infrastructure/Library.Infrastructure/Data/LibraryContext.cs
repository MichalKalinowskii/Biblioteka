using Library.Domain.Books;
using Library.Domain.Clients;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Library.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Rentals;
using Library.Domain.Staff;
using Library.Infrastructure.Domain.Clients;
using Library.Infrastructure.Domain.Rentals;
using Library.Infrastructure.Domain.Staff;
using Library.Domain.Books.Models;
using Library.Domain.BookCopies.Models;
using Library.Infrastructure.Domain.Books;
using Library.Infrastructure.Domain.BookCopies;
using Library.Domain.Authors.Models;
using Library.Domain.Locations.Models;
using Library.Infrastructure.Domain.Authors;
using Library.Infrastructure.Domain.Locations;

namespace Library.Infrastructure.Data;

public class LibraryContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public virtual DbSet<Rental> Rentals { get; set; }
    public virtual DbSet<BookRental> BookRentals { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<BookCopy> BookCopies { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<BookCopyStatus> BookCopyStatuses { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<AuthorBook> AuthorBooks { get; set; }
    public virtual DbSet<Location> Locations { get; set; }

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
        builder.ApplyConfiguration(new ClientEntityConfiguration());
        builder.ApplyConfiguration(new BookEntityConfiguration());
        builder.ApplyConfiguration(new BookCopyEntityConfiguration());
        builder.ApplyConfiguration(new GenreEntityConfiguration());
        builder.ApplyConfiguration(new AuthorEntityConfiguration());
        builder.ApplyConfiguration(new AuthorBookEntityConfiguration());
        builder.ApplyConfiguration(new LocationEntityConfiguration());
    }
}