using Library.Application;
using Library.Application.Authentication;
using Library.Application.Authentication.Register;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Authentication;

public class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("authentication/register", async ([FromBody] RegisterRequestDto registerRequestDto, AuthenticationService authenticationService, CancellationToken cancellationToken) =>
        {
            var result = await authenticationService.RegisterAsync(registerRequestDto, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(result);
            }

            return Results.Ok(result);
        })
        .WithTags(Tags.Authentication);
    }
}