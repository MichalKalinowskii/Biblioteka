using Library.Domain.Books.Interfaces;
using Library.Domain.SeedWork;
using Library.Infrastructure.Authentication;
using Library.Infrastructure.Data;
using Library.Infrastructure.Domain;
using Library.Infrastructure.Domain.Books;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure;

public static class Registration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LibraryContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("SqlDockerDevelopmentConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<LibraryContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBookPersistence, BookRepository>();
        
        services.AddJwtAuthentication(configuration);
    }
}