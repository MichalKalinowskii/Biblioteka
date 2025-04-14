using Library.Application.Rentals;
using Library.Domain.Rentals;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Rentals;

public class CreateRental : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("", async ([FromBody] CreateRentalDto createRentalDto, RentalService rentalService, CancellationToken cancellationToken) =>
            {
                var result = await rentalService.CreateRentalAsync(createRentalDto.LibraryCardId, createRentalDto.EmployeeId, createRentalDto.BookCopyIds, createRentalDto.ReturnDate, cancellationToken);

                if (result.IsFailure || result.Value is null)
                {
                    return Results.BadRequest(result);
                }

                return Results.Created($"{result.Value.Id}", result.Value);
            })
            .WithTags(Tags.Rentals);
    }
}