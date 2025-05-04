using Library.Application.Rentals;
using Library.Domain.Rentals;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Rentals;

public class CreateRental : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("rentals", async Task<Results<Ok<Rental>, BadRequest<Result<Rental>>>> ([FromBody] CreateRentalDto createRentalDto, IAuthenticatedUserService authenticatedUserService, RentalService rentalService, CancellationToken cancellationToken) =>
            {
                Guid userId = (Guid)authenticatedUserService.UserId!;
                
                var result = await rentalService.CreateRentalAsync(createRentalDto.LibraryCardId, userId, createRentalDto.BookCopyIds, createRentalDto.ReturnDate, cancellationToken);

                if (result.IsFailure || result.Value is null)
                {
                    return TypedResults.BadRequest(result);
                }

                return TypedResults.Ok(result.Value);
            })
            .RequireAuthorization("employee")
            .WithDescription("Bibliotekarz - tworzenie nowego wypożyczenia - zakładam, że client daje kartę biblioteczną fizycznie, do testów można wziąć z GetClients.")
            .WithTags(Tags.Rentals);
    }
}