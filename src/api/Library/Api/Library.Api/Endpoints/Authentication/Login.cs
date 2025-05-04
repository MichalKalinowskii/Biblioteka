using Library.Application;
using Library.Application.Authentication;
using Library.Application.Authentication.Login;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Authentication;

public class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("authentication/login", async Task<Results<Ok<Result<LoginResponseDto>>, BadRequest<Result<LoginResponseDto>>>> ([FromBody] LoginRequestDto loginRequestDto, AuthenticationService authenticationService, CancellationToken cancellationToken) =>
        {
            Result<LoginResponseDto> result = await authenticationService.LoginAsync(loginRequestDto, cancellationToken);

            if (result.IsFailure)
            {
                return TypedResults.BadRequest(result);
            }

            return TypedResults.Ok(result);
        })
        .WithDescription("Logowanie")
        .WithTags(Tags.Authentication);
    }
}