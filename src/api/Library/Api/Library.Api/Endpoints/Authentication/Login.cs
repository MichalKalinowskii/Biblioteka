using Library.Application;
using Library.Application.Authentication;
using Library.Application.Authentication.Login;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Authentication;

public class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("authentication/login", async ([FromBody] LoginRequestDto loginRequestDto, AuthenticationService authenticationService, CancellationToken cancellationToken) =>
        {
            var result = await authenticationService.LoginAsync(loginRequestDto, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(result);
            }

            return Results.Ok(result);
        })
        .WithTags(Tags.Authentication);
    }
}