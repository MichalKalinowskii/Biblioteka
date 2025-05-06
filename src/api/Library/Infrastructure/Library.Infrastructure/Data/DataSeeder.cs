#nullable disable

using Microsoft.Extensions.DependencyInjection;
using Library.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Library.Domain.Books;
using Library.Domain.BookCopies;
using Library.Domain.Books.Models;
using Library.Domain.BookCopies.Models;
using Library.Domain.Clients;
using Library.Domain.Rentals;
using Library.Domain.Staff;
using Library.Domain.Locations.Models;
using Library.Domain.Authors.Models;
using Library.Domain.Authors;

namespace Library.Infrastructure.Data
{
    public class DataSeeder(IServiceProvider serviceProvider)
    {
        private LibraryContext _dbContext;
        private PasswordHasher<ApplicationUser> _passwordHasher;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole<Guid>> _roleManager;
        private BookService _bookService;
        private BookCopyService _bookCopyService;
        private AuthorService _authorService;

        public async Task SeedDataAsync()
        {
            using var scope = serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            _passwordHasher = new PasswordHasher<ApplicationUser>();
            _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _bookService =  scope.ServiceProvider.GetRequiredService<BookService>();
            _bookCopyService = scope.ServiceProvider.GetRequiredService<BookCopyService>();
            _authorService = scope.ServiceProvider.GetRequiredService<AuthorService>();
            _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
           
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

            var locations = await AddLocations();

            var authors = await AddAuthors();

            await _dbContext.SaveChangesAsync();
        }


        private Guid location1 = Guid.NewGuid();
        private Guid location2 = Guid.NewGuid();
        private Guid location3 = Guid.NewGuid();
        private Guid location4 = Guid.NewGuid();
        private Guid location5 = Guid.NewGuid();
        private Guid location6 = Guid.NewGuid();
        private async Task<List<Location>> AddLocations()
        {
            List<Location> locations = new List<Location>();

            locations.Add(new Location(location1, 1, 1, 1, "First Floor, Zone 1, Shelf 1", "Red"));
            locations.Add(new Location(location2, 1, 2, 1, "First Floor, Zone 1, Shelf 2", "Blue"));
            locations.Add(new Location(location3, 2, 1, 2, "Second Floor, Zone 2, Shelf 1", "Green"));
            locations.Add(new Location(location4, 2, 2, 2, "Second Floor, Zone 2, Shelf 2", "Yellow"));
            locations.Add(new Location(location5, 88, 88, 88, "Damaged", "Damaged"));
            locations.Add(new Location(location6, 99, 99, 99, "Rented", "Rented"));

            await _dbContext.Set<Location>().AddRangeAsync(locations);

            return locations;
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
                FirstName = "Employee",
                LastName = "Test",
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
                FirstName = "ClientA",
                LastName = "Test",
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
                FirstName = "ClientB",
                LastName = "Test",
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
            await _bookService.AddNewBookAsync("Penguin Random House",
               "To Kill a Mockingbird",
               "https://example.com/tokillamockingbird.jpg",
               new DateTime(1960, 7, 11),
               "A novel about the serious issues racial inequality.",
               "9780061120084",
               Genre.Drama,
               new CancellationToken());

            await _bookService.AddNewBookAsync("HarperCollins",
               "1984",
               "https://example.com/1984.jpg",
               new DateTime(1949, 6, 8),
               "A dystopian social science fiction novel and cautionary tale about the dangers of totalitarianism.",
               "9780451524935",
               Genre.Thriller,
               new CancellationToken());

            await _bookService.AddNewBookAsync("Scholastic",
               "Harry Potter and the Sorcerer's Stone",
               "https://example.com/harrypotter1.jpg",
               new DateTime(1997, 6, 26),
               "The first book in the Harry Potter series, introducing the wizarding world.",
               "9780590353427",
               Genre.SinceFiction,
               new CancellationToken());

            await _bookService.AddNewBookAsync("Scribner",
               "The Great Gatsby",
               "https://example.com/thegreatgatsby.jpg",
               new DateTime(1925, 4, 10),
               "A novel about the American dream and the disillusionment of the Jazz Age.",
               "9780743273565",
               Genre.Drama,
               new CancellationToken());

            await _bookService.AddNewBookAsync("Little, Brown and Company",
               "The Catcher in the Rye",
               "https://example.com/thecatcherintherye.jpg",
               new DateTime(1951, 7, 16),
               "A story about teenage rebellion and alienation.",
               "9780316769488",
               Genre.Drama,
               new CancellationToken());

            await _bookService.AddNewBookAsync("Houghton Mifflin Harcourt",
               "The Hobbit",
               "https://example.com/thehobbit.jpg",
               new DateTime(1937, 9, 21),
               "A fantasy novel about the adventures of Bilbo Baggins.",
               "9780547928227",
               Genre.SinceFiction,
               new CancellationToken());
        }

        private async Task<List<BookCopy>> AddBookCopies()
        {
            List<BookCopy> bookCopies = new List<BookCopy>();

            // Add book copies for "To Kill a Mockingbird"
            var book1 = await _bookService.GetBookByISBN("9780061120084", new CancellationToken());
            var book1Copy1 = await _bookCopyService.AddNewBookCopyAsync(book1.Value.Id, location1, BookCopyStatus.Available, new CancellationToken());
            var book1Copy2 = await _bookCopyService.AddNewBookCopyAsync(book1.Value.Id, location1, BookCopyStatus.Damaged, new CancellationToken());
            var book1Copy3 = await _bookCopyService.AddNewBookCopyAsync(book1.Value.Id, location1, BookCopyStatus.Reserved, new CancellationToken());
            bookCopies.Add(book1Copy1.Value);
            bookCopies.Add(book1Copy2.Value);
            bookCopies.Add(book1Copy3.Value);

            // Add book copies for "1984"
            var book2 = await _bookService.GetBookByISBN("9780451524935", new CancellationToken());
            var book2Copy1 = await _bookCopyService.AddNewBookCopyAsync(book2.Value.Id, location2, BookCopyStatus.Available, new CancellationToken());
            var book2Copy2 = await _bookCopyService.AddNewBookCopyAsync(book2.Value.Id, location2, BookCopyStatus.Lost, new CancellationToken());
            var book2Copy3 = await _bookCopyService.AddNewBookCopyAsync(book2.Value.Id, location2, BookCopyStatus.Reserved, new CancellationToken());
            bookCopies.Add(book2Copy1.Value);
            bookCopies.Add(book2Copy2.Value);
            bookCopies.Add(book2Copy3.Value);

            // Add book copies for "Harry Potter and the Sorcerer's Stone"
            var book3 = await _bookService.GetBookByISBN("9780590353427", new CancellationToken());
            var book3Copy1 = await _bookCopyService.AddNewBookCopyAsync(book3.Value.Id, location2, BookCopyStatus.Available, new CancellationToken());
            var book3Copy2 = await _bookCopyService.AddNewBookCopyAsync(book3.Value.Id, location3, BookCopyStatus.Damaged, new CancellationToken());
            var book3Copy3 = await _bookCopyService.AddNewBookCopyAsync(book3.Value.Id, location3, BookCopyStatus.Reserved, new CancellationToken());
            bookCopies.Add(book3Copy1.Value);
            bookCopies.Add(book3Copy2.Value);
            bookCopies.Add(book3Copy3.Value);

            // Add book copies for "The Great Gatsby"
            var book4 = await _bookService.GetBookByISBN("9780743273565", new CancellationToken());
            var book4Copy1 = await _bookCopyService.AddNewBookCopyAsync(book4.Value.Id, location4, BookCopyStatus.Available, new CancellationToken());
            var book4Copy2 = await _bookCopyService.AddNewBookCopyAsync(book4.Value.Id, location4, BookCopyStatus.Unavailable, new CancellationToken());
            var book4Copy3 = await _bookCopyService.AddNewBookCopyAsync(book4.Value.Id, location4, BookCopyStatus.Reserved, new CancellationToken());
            bookCopies.Add(book4Copy1.Value);
            bookCopies.Add(book4Copy2.Value);
            bookCopies.Add(book4Copy3.Value);

            // Add book copies for "The Catcher in the Rye"
            var book5 = await _bookService.GetBookByISBN("9780316769488", new CancellationToken());
            var book5Copy1 = await _bookCopyService.AddNewBookCopyAsync(book5.Value.Id, location1, BookCopyStatus.Available, new CancellationToken());
            var book5Copy2 = await _bookCopyService.AddNewBookCopyAsync(book5.Value.Id, location2, BookCopyStatus.Damaged, new CancellationToken());
            var book5Copy3 = await _bookCopyService.AddNewBookCopyAsync(book5.Value.Id, location3, BookCopyStatus.Reserved, new CancellationToken());
            bookCopies.Add(book5Copy1.Value);
            bookCopies.Add(book5Copy2.Value);
            bookCopies.Add(book5Copy3.Value);

            // Add book copies for "The Hobbit"
            var book6 = await _bookService.GetBookByISBN("9780547928227", new CancellationToken());
            var book6Copy1 = await _bookCopyService.AddNewBookCopyAsync(book6.Value.Id, location2, BookCopyStatus.Available, new CancellationToken());
            var book6Copy2 = await _bookCopyService.AddNewBookCopyAsync(book6.Value.Id, location3, BookCopyStatus.Lost, new CancellationToken());
            var book6Copy3 = await _bookCopyService.AddNewBookCopyAsync(book6.Value.Id, location4, BookCopyStatus.Reserved, new CancellationToken());
            bookCopies.Add(book6Copy1.Value);
            bookCopies.Add(book6Copy2.Value);
            bookCopies.Add(book6Copy3.Value);

            return bookCopies;
        }

        private async Task<List<Author>> AddAuthors()
        {
            List<Author> authors = new List<Author>();

            // Add authors for "To Kill a Mockingbird"  
            var author1Result = await _authorService.AddNewAuthor(
                name: "Harper",
                lastName: "Lee",
                dateOfBirth: new DateTime(1926, 4, 28, 0, 0, 0, DateTimeKind.Utc),
                dateOfDeath: new DateTime(2016, 2, 19, 0, 0, 0, DateTimeKind.Utc),
                cancellationToken: new CancellationToken(),
                description: "Author of 'To Kill a Mockingbird', a classic novel about racial inequality."
            );
            if (author1Result.IsSuccess)
            {
                authors.Add(author1Result.Value);
                var book1 = await _bookService.GetBookByISBN("9780061120084", new CancellationToken());
                await _dbContext.Set<AuthorBook>().AddAsync(new AuthorBook(
                    Guid.NewGuid(), // Unique ID for the AuthorBook relationship  
                    author1Result.Value.Id,
                    book1.Value.Id
                ));
            }

            // Add authors for "1984"  
            var author2Result = await _authorService.AddNewAuthor(
                name: "George",
                lastName: "Orwell",
                dateOfBirth: new DateTime(1903, 6, 25, 0, 0, 0, DateTimeKind.Utc),
                dateOfDeath: new DateTime(1950, 1, 21, 0, 0, 0, DateTimeKind.Utc),
                cancellationToken: new CancellationToken(),
                description: "Author of '1984', a dystopian novel about totalitarianism."
            );
            if (author2Result.IsSuccess)
            {
                authors.Add(author2Result.Value);
                var book2 = await _bookService.GetBookByISBN("9780451524935", new CancellationToken());
                await _dbContext.Set<AuthorBook>().AddAsync(new AuthorBook(
                    Guid.NewGuid(),
                    author2Result.Value.Id,
                    book2.Value.Id
                ));
            }

            // Add authors for "Harry Potter and the Sorcerer's Stone"  
            var author3Result = await _authorService.AddNewAuthor(
                name: "J.K.",
                lastName: "Rowling",
                dateOfBirth: new DateTime(1965, 7, 31, 0, 0, 0, DateTimeKind.Utc),
                dateOfDeath: DateTime.MinValue, // Still alive  
                cancellationToken: new CancellationToken(),
                description: "Author of the Harry Potter series, introducing the wizarding world."
            );
            if (author3Result.IsSuccess)
            {
                authors.Add(author3Result.Value);
                var book3 = await _bookService.GetBookByISBN("9780590353427", new CancellationToken());
                await _dbContext.Set<AuthorBook>().AddAsync(new AuthorBook(
                    Guid.NewGuid(),
                    author3Result.Value.Id,
                    book3.Value.Id
                ));
            }

            // Add authors for "The Great Gatsby"  
            var author4Result = await _authorService.AddNewAuthor(
                name: "F. Scott",
                lastName: "Fitzgerald",
                dateOfBirth: new DateTime(1896, 9, 24, 0, 0, 0, DateTimeKind.Utc),
                dateOfDeath: new DateTime(1940, 12, 21, 0, 0, 0, DateTimeKind.Utc),
                cancellationToken: new CancellationToken(),
                description: "Author of 'The Great Gatsby', a novel about the American dream."
            );
            if (author4Result.IsSuccess)
            {
                authors.Add(author4Result.Value);
                var book4 = await _bookService.GetBookByISBN("9780743273565", new CancellationToken());
                await _dbContext.Set<AuthorBook>().AddAsync(new AuthorBook(
                    Guid.NewGuid(),
                    author4Result.Value.Id,
                    book4.Value.Id
                ));
            }

            // Add authors for "The Catcher in the Rye"  
            var author5Result = await _authorService.AddNewAuthor(
                name: "J.D.",
                lastName: "Salinger",
                dateOfBirth: new DateTime(1919, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                dateOfDeath: new DateTime(2010, 1, 27, 0, 0, 0, DateTimeKind.Utc),
                cancellationToken: new CancellationToken(),
                description: "Author of 'The Catcher in the Rye', a story about teenage rebellion."
            );
            if (author5Result.IsSuccess)
            {
                authors.Add(author5Result.Value);
                var book5 = await _bookService.GetBookByISBN("9780316769488", new CancellationToken());
                await _dbContext.Set<AuthorBook>().AddAsync(new AuthorBook(
                    Guid.NewGuid(),
                    author5Result.Value.Id,
                    book5.Value.Id
                ));
            }

            // Add authors for "The Hobbit"  
            var author6Result = await _authorService.AddNewAuthor(
                name: "J.R.R.",
                lastName: "Tolkien",
                dateOfBirth: new DateTime(1892, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                dateOfDeath: new DateTime(1973, 9, 2, 0, 0, 0, DateTimeKind.Utc),
                cancellationToken: new CancellationToken(),
                description: "Author of 'The Hobbit', a fantasy novel about the adventures of Bilbo Baggins."
            );
            if (author6Result.IsSuccess)
            {
                authors.Add(author6Result.Value);
                var book6 = await _bookService.GetBookByISBN("9780547928227", new CancellationToken());
                await _dbContext.Set<AuthorBook>().AddAsync(new AuthorBook(
                    Guid.NewGuid(),
                    author6Result.Value.Id,
                    book6.Value.Id
                ));
            }

            await _dbContext.SaveChangesAsync();
            return authors;
        }


        private async Task<List<Employee>> AddEmployees(List<ApplicationUser> applicationUsers)
        {
            List<Employee> employees = new List<Employee>();

            await _roleManager.CreateAsync(new IdentityRole<Guid>("Employee"));

            employees.Add(Employee.Create(applicationUsers[0].Id).Value);
            
            await _dbContext.Set<Employee>().AddRangeAsync(employees);
            
            await _dbContext.SaveChangesAsync();

            await _userManager.AddToRoleAsync(applicationUsers[0], "Employee");
            
            return employees;
        }

        private async Task<List<Client>> AddClients(List<ApplicationUser> applicationUsers)
        {
            List<Client> clients = new List<Client>();

            await _roleManager.CreateAsync(new IdentityRole<Guid>("Client"));

            clients.Add(Client.Create(applicationUsers[1].Id, Guid.NewGuid()).Value);
            clients.Add(Client.Create(applicationUsers[2].Id, Guid.NewGuid()).Value);
            
            await _dbContext.Set<Client>().AddRangeAsync(clients);
            
            await _dbContext.SaveChangesAsync();

            await _userManager.AddToRoleAsync(applicationUsers[1], "Client");
            await _userManager.AddToRoleAsync(applicationUsers[2], "Client");

            return clients;
        }

        private async Task<List<Rental>> AddRentals(List<Employee> employees, List<Client> clients, List<BookCopy> bookCopies)
        {
            List<Rental> rentals = new List<Rental>();
            
            var rental = Rental.Create(clients[0].LibraryCardId, employees[0].UserId, new List<Guid>
            {
                bookCopies[0].Id,
                bookCopies[3].Id
            }, DateTime.UtcNow.AddDays(14));

            rentals.Add(rental.Value);

            rental = Rental.Create(clients[1].LibraryCardId, employees[0].UserId, new List<Guid>
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
