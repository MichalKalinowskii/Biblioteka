using Library.Domain.Clients;
using Library.Domain.Rentals;
using Library.Domain.SeedWork;
using Library.Infrastructure.Authentication;
using Library.Infrastructure.Data;
using Library.Infrastructure.Domain;
using Library.Infrastructure.Domain.Clients;
using Library.Infrastructure.Domain.Rentals;
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
        
        services.AddJwtAuthentication(configuration);
        services.AddRepositories();
        services.AddDomainServices();
    }

    private static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<RentalService>();
    }
    
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
    }
}