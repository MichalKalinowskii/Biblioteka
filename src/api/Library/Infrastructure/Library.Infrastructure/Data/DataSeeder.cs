#nullable disable

using Microsoft.Extensions.DependencyInjection;
using Library.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Data
{
    public class DataSeeder(IServiceProvider serviceProvider)
    {
        private LibraryContext _dbContext;
        private PasswordHasher<ApplicationUser> _passwordHasher;
        private UserManager<ApplicationUser> _userManager;

        public async Task SeedDataAsync()
        {
            using var scope = serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            _passwordHasher = new PasswordHasher<ApplicationUser>();
            _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedData();
        }

        private async Task SeedData()
        {
            await _dbContext.Database.EnsureDeletedAsync();

            await _dbContext.Database.EnsureCreatedAsync();

            List<ApplicationUser> applicationUsers = await AddApplicationUsers();
            
            

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
    }
}
