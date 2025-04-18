using Library.Application.Rentals;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Rentals;

namespace Library.Api.Endpoints.Rentals;

public class ReturnBooks : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("rentals/return-books", async ([FromBody] ReturnBooksDto returnBooksDto, RentalService rentalService, CancellationToken cancellationToken) =>
            {
                var result = await rentalService.ReturnBookAsync(returnBooksDto.LibraryCardId, returnBooksDto.BookCopyIds, cancellationToken);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result);
                }

                return Results.Ok();
            })
            .WithTags(Tags.Rentals);
    }
}