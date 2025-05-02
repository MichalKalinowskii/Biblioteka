using Library.Domain.Authors;
using Library.Domain.Clients;
using Library.Domain.Rentals;
using Library.Domain.BookCopies.Interfaces;
using Library.Domain.Books.Interfaces;
using Library.Domain.SeedWork;
using Library.Infrastructure.Authentication;
using Library.Infrastructure.Data;
using Library.Infrastructure.Domain;
using Library.Infrastructure.Domain.BookCopies;
using Library.Infrastructure.Domain.Authors;
using Library.Infrastructure.Domain.Books;
using Library.Infrastructure.Domain.Clients;
using Library.Infrastructure.Domain.Rentals;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Library.Domain.Books;
using Library.Domain.BookCopies;

namespace Library.Infrastructure;

public static class Registration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        string connectionString = string.Empty;

        if (isDevelopment)
        {
            connectionString = configuration.GetConnectionString("SqlDockerDevelopmentConnection");
        }
        else
        {
            connectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_Database");
        }

        services.AddDbContext<LibraryContext>(options =>
    options.UseNpgsql(connectionString));

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<LibraryContext>()
            .AddDefaultTokenProviders();

        services.AddJwtAuthentication(configuration);
        services.AddRepositories();
        services.AddDomainServices();
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

        services.AddScoped<SqlConnectionFactory>(x => new SqlConnectionFactory(connectionString));


        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<RentalService>();
        services.AddScoped<BookCopyService>();
        services.AddScoped<BookService>();
        services.AddScoped<AuthorService>();
    }
    
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IBookPersistence, BookRepository>();
        services.AddScoped<IAuthorPersistance, AuthorRepository>();
        services.AddScoped<IBookCopyPersistance, BookCopyRepository>();
    }
}