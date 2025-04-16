#nullable disable

using Microsoft.Extensions.DependencyInjection;
using Library.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Library.Domain.Books;
using Library.Domain.BookCopies;
using Library.Domain.Books.Models;
using Library.Domain.Books.Entites;
using Library.Domain.BookCopies.Models;
using Library.Domain.Clients;
using Library.Domain.Rentals;
using Library.Domain.Staff;

namespace Library.Infrastructure.Data
{
    public class DataSeeder(IServiceProvider serviceProvider)
    {
        private LibraryContext _dbContext;
        private PasswordHasher<ApplicationUser> _passwordHasher;
        private UserManager<ApplicationUser> _userManager;
        private BookService _bookService;
        private BookCopyService _bookCopyService;

        public async Task SeedDataAsync()
        {
            using var scope = serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            _passwordHasher = new PasswordHasher<ApplicationUser>();
            _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _bookService =  scope.ServiceProvider.GetRequiredService<BookService>();
            _bookCopyService = scope.ServiceProvider.GetRequiredService<BookCopyService>();

            await SeedData();
        }

        private async Task SeedData()
        {
            await _dbContext.Database.EnsureDeletedAsync();

            await _dbContext.Database.EnsureCreatedAsync();

            List<ApplicationUser> applicationUsers = await AddApplicationUsers();

            await AddBooks();

            var bookCopies = await AddBookCopies();

            var employees = await AddEmployees(applicationUsers);
            
            var clients = await AddClients(applicationUsers);

            var rentals = await AddRentals(employees, clients, bookCopies);
            
            await _dbContext.SaveChangesAsync();
        }

        private async Task<List<ApplicationUser>> AddApplicationUsers()
        {
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
            
            string userEmail = "seededUser1@test.com";
            string userPassword = "TestPassword123!";
            
            applicationUsers.Add(new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = userEmail,
                FirstName = "Test",
                LastName = "User",
                NormalizedEmail = userEmail.ToUpper(),
                UserName = "testuser1@example.com",
                PasswordHash = _passwordHasher.HashPassword(default, userPassword)
            });
            
            string user2Email = "seededUser2@test.com";
            string user2Password = "TestPassword123!";
            
            applicationUsers.Add(new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = user2Email,
                FirstName = "Test",
                LastName = "User",
                NormalizedEmail = user2Email.ToUpper(),
                UserName = "testuser2@example.com",
                PasswordHash = _passwordHasher.HashPassword(default, user2Password)
            });
            
            string user3Email = "seededUser3@test.com";
            string user3Password = "TestPassword123!";
            
            applicationUsers.Add(new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = user3Email,
                FirstName = "Test",
                LastName = "User",
                NormalizedEmail = user3Email.ToUpper(),
                UserName = "testuser3@example.com",
                PasswordHash = _passwordHasher.HashPassword(default, user3Password)
            });

            await _userManager.CreateAsync(applicationUsers[0]);
            await _userManager.CreateAsync(applicationUsers[1]);
            await _userManager.CreateAsync(applicationUsers[2]);
            
            return applicationUsers;
        }

        private async Task AddBooks()
        {
            await _bookService.AddNewBookAsync("PAKA",
                "Book 1",
                "https://example.com/book1.jpg",
                DateTime.Now,
                "Description of Book 1",
                "1234567890123",
                Genre.Comedy,             
                new CancellationToken());

            await _bookService.AddNewBookAsync("PAKA",
                "Book 2",
                "https://example.com/book2.jpg",
                DateTime.Now,
                "Description of Book 2",
                "1234567890124",
                Genre.Horror,
                new CancellationToken());

            await _bookService.AddNewBookAsync("PAKA",
                "Book 3",
                "https://example.com/book3.jpg",
                DateTime.Now,
                "Description of Book 3",
                "1234567890125",
                Genre.Romance,
                new CancellationToken());
        }

        private async Task<List<BookCopy>> AddBookCopies()
        {
            List<BookCopy> bookCopies = new List<BookCopy>();
            
            var book = await _bookService.GetBookByISBN("1234567890123", new CancellationToken());

            var bookCopy1 =  await _bookCopyService.AddNewBookCopyAsync(book.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Available, new CancellationToken());

            var bookCopy2 = await _bookCopyService.AddNewBookCopyAsync(book.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Damaged, new CancellationToken());

            var bookCopy3 = await _bookCopyService.AddNewBookCopyAsync(book.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Reserved, new CancellationToken());
            
            bookCopies.Add(bookCopy1.Value);
            bookCopies.Add(bookCopy2.Value);
            bookCopies.Add(bookCopy3.Value);

            var book2 = await _bookService.GetBookByISBN("1234567890124", new CancellationToken());

            var bookCopy4 = await _bookCopyService.AddNewBookCopyAsync(book2.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Available, new CancellationToken());

            var bookCopy5 = await _bookCopyService.AddNewBookCopyAsync(book2.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Available, new CancellationToken());

            var bookCopy6 = await _bookCopyService.AddNewBookCopyAsync(book2.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Lost, new CancellationToken());
            
            bookCopies.Add(bookCopy4.Value);
            bookCopies.Add(bookCopy5.Value);
            bookCopies.Add(bookCopy6.Value);

            var book3 = await _bookService.GetBookByISBN("1234567890125", new CancellationToken());

            var bookCopy7 = await _bookCopyService.AddNewBookCopyAsync(book3.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Available, new CancellationToken());

            var bookCopy8 = await _bookCopyService.AddNewBookCopyAsync(book3.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Available, new CancellationToken());

            var bookCopy9 = await _bookCopyService.AddNewBookCopyAsync(book3.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Unavailable, new CancellationToken());
            
            bookCopies.Add(bookCopy7.Value);
            bookCopies.Add(bookCopy8.Value);
            bookCopies.Add(bookCopy9.Value);
            
            return bookCopies;
        }

        private async Task<List<Employee>> AddEmployees(List<ApplicationUser> applicationUsers)
        {
            List<Employee> employees = new List<Employee>();
            
            employees.Add(Employee.Create(applicationUsers[0].Id).Value);
            
            await _dbContext.Set<Employee>().AddRangeAsync(employees);
            
            await _dbContext.SaveChangesAsync();
            
            return employees;
        }

        private async Task<List<Client>> AddClients(List<ApplicationUser> applicationUsers)
        {
            List<Client> clients = new List<Client>();
            
            clients.Add(Client.Create(applicationUsers[1].Id, Guid.NewGuid()).Value);
            clients.Add(Client.Create(applicationUsers[2].Id, Guid.NewGuid()).Value);
            
            await _dbContext.Set<Client>().AddRangeAsync(clients);
            
            await _dbContext.SaveChangesAsync();
            
            return clients;
        }

        private async Task<List<Rental>> AddRentals(List<Employee> employees, List<Client> clients, List<BookCopy> bookCopies)
        {
            List<Rental> rentals = new List<Rental>();
            
            var rental = Rental.Create(clients[0].LibraryCardId, employees[0].Id, new List<Guid>
            {
                bookCopies[0].Id,
                bookCopies[3].Id
            }, DateTime.UtcNow.AddDays(14));

            rentals.Add(rental.Value);

            rental = Rental.Create(clients[1].LibraryCardId, employees[0].Id, new List<Guid>
            {
                bookCopies[4].Id,
                bookCopies[7].Id
            }, DateTime.UtcNow.AddDays(14));
            
            rentals.Add(rental.Value);
            
            await _dbContext.Set<Rental>().AddRangeAsync(rentals);
            
            return rentals;
        }
    }
}
