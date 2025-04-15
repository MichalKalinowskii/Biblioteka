#nullable disable

using Microsoft.Extensions.DependencyInjection;
using Library.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Library.Domain.Books;
using Library.Domain.BookCopies;
using Library.Domain.Books.Models;
using Library.Domain.Books.Entites;
using Library.Domain.BookCopies.Models;

namespace Library.Infrastructure.Data
{
    public class DataSeeder(IServiceProvider serviceProvider)
    {
        private LibraryContext _dbContext;
        private PasswordHasher<ApplicationUser> _passwordHasher;
        private UserManager<ApplicationUser> _userManager;
        private BookService bookService;
        private BookCopyService bookCopyService;

        public async Task SeedDataAsync()
        {
            using var scope = serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            _passwordHasher = new PasswordHasher<ApplicationUser>();
            _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            bookService =  scope.ServiceProvider.GetRequiredService<BookService>();
            bookCopyService = scope.ServiceProvider.GetRequiredService<BookCopyService>();

            await SeedData();
        }

        private async Task SeedData()
        {
            await _dbContext.Database.EnsureDeletedAsync();

            await _dbContext.Database.EnsureCreatedAsync();

            List<ApplicationUser> applicationUsers = await AddApplicationUsers();

            await AddBooks();

            await AddBookCopies();

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
                UserName = "testuser@example.com",
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
                UserName = "testuser@example.com",
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
                UserName = "testuser@example.com",
                PasswordHash = _passwordHasher.HashPassword(default, user3Password)
            });

            await _userManager.CreateAsync(applicationUsers[0]);
            await _userManager.CreateAsync(applicationUsers[1]);
            await _userManager.CreateAsync(applicationUsers[2]);

            return applicationUsers;
        }

        private async Task AddBooks()
        {
            var book1 = new BookEntity
            {
                Title = "Book 1",
                TitlePageImageUrl = "https://example.com/book1.jpg",
                Genre = Genre.Comedy,
                ReleaseDate = DateTime.UtcNow,
                Description = "Description of Book 1",
                ISBN = "1234567890123",
                Publisher = "PAKA",
            };

            await bookService.AddNewBookAsync(book1, new CancellationToken());

            var book2 = new BookEntity
            {
                Title = "Book 2",
                TitlePageImageUrl = "https://example.com/book2.jpg",
                Genre = Genre.Horror,
                ReleaseDate = DateTime.UtcNow,
                Description = "Description of Book 2",
                ISBN = "1234567890124",
                Publisher = "PAKA",
            };

            await bookService.AddNewBookAsync(book2, new CancellationToken());

            var book3 = new BookEntity
            {
                Title = "Book 3",
                TitlePageImageUrl = "https://example.com/book3.jpg",
                Genre = Genre.Romance,
                ReleaseDate = DateTime.UtcNow,
                Description = "Description of Book 3",
                ISBN = "1234567890125",
                Publisher = "PAKA",
            };

            await bookService.AddNewBookAsync(book3, new CancellationToken());
        }

        private async Task AddBookCopies()
        {
            var book = await bookService.GetBookByISBN("1234567890123", new CancellationToken());

            await bookCopyService.AddNewBookCopyAsync(book.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Available, new CancellationToken());

            await bookCopyService.AddNewBookCopyAsync(book.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Damaged, new CancellationToken());

            await bookCopyService.AddNewBookCopyAsync(book.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Reserved, new CancellationToken());

            var book2 = await bookService.GetBookByISBN("1234567890124", new CancellationToken());

            await bookCopyService.AddNewBookCopyAsync(book2.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Available, new CancellationToken());

            await bookCopyService.AddNewBookCopyAsync(book2.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Available, new CancellationToken());

            await bookCopyService.AddNewBookCopyAsync(book2.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Lost, new CancellationToken());

            var book3 = await bookService.GetBookByISBN("1234567890125", new CancellationToken());

            await bookCopyService.AddNewBookCopyAsync(book3.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Available, new CancellationToken());

            await bookCopyService.AddNewBookCopyAsync(book3.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Available, new CancellationToken());

            await bookCopyService.AddNewBookCopyAsync(book3.Value.Id, Guid.NewGuid(),
                BookCopyStatus.Unavailable, new CancellationToken());
        }
    }
}
