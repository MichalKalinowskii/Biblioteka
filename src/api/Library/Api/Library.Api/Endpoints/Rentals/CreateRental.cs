using Library.Application.Rentals;
using Library.Domain.Rentals;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Rentals;

public class CreateRental : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("rentals", async ([FromBody] CreateRentalDto createRentalDto, IAuthenticatedUserService authenticatedUserService, RentalService rentalService, CancellationToken cancellationToken) =>
            {
                Guid userId = (Guid)authenticatedUserService.UserId!;
                
                var result = await rentalService.CreateRentalAsync(createRentalDto.LibraryCardId, userId, createRentalDto.BookCopyIds, createRentalDto.ReturnDate, cancellationToken);

                if (result.IsFailure || result.Value is null)
                {
                    return Results.BadRequest(result);
                }

                return Results.Created($"{result.Value.Id}", result.Value);
            })
            .RequireAuthorization("employee")
            .WithTags(Tags.Rentals);
    }
}