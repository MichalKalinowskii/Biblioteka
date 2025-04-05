using Library.Application.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Application;

public static class Registration
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationService>();
    }
}