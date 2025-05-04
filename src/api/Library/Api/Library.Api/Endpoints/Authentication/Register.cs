using Library.Application;
using Library.Application.Authentication;
using Library.Application.Authentication.Register;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Authentication;

public class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("authentication/register", async Task<Results<Ok<Result>, BadRequest<Result>>> ([FromBody] RegisterRequestDto registerRequestDto, AuthenticationService authenticationService, CancellationToken cancellationToken) =>
        {
            var result = await authenticationService.RegisterAsync(registerRequestDto, cancellationToken);

            if (result.IsFailure)
            {
                return TypedResults.BadRequest(result);
            }

            return TypedResults.Ok(result);
        })
        .WithDescription("Rejestracja - obecnie tworzy nowego clienta.")
        .WithTags(Tags.Authentication);
    }
}