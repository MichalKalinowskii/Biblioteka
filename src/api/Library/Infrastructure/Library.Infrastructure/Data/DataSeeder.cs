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

            ApplicationUser applicationUser = await AddApplicationUser();

            await _dbContext.SaveChangesAsync();
        }

        private async Task<ApplicationUser> AddApplicationUser()
        {
            string userEmail = "seededUser1@test.com";
            string userPassword = "TestPassword123!";
            var user = new ApplicationUser
            {
                Id = 0,
                Email = userEmail,
                FirstName = "Test",
                LastName = "User",
                NormalizedEmail = userEmail.ToUpper(),
                UserName = "testuser@example.com",
                PasswordHash = _passwordHasher.HashPassword(default, userPassword)
            };

            await _userManager.CreateAsync(user);

            return user;
        }
    }
}
