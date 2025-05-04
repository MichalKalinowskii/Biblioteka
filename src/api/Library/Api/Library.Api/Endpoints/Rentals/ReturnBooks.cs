using Library.Application.Rentals;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Rentals;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Rentals;

public class ReturnBooks : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("rentals/return-books", async Task<Results<Ok<Result>, BadRequest<Result>>> ([FromBody] ReturnBooksDto returnBooksDto, RentalService rentalService, CancellationToken cancellationToken) =>
            {
                var result = await rentalService.ReturnBookAsync(returnBooksDto.LibraryCardId, returnBooksDto.BookCopyIds, cancellationToken);

                if (result.IsFailure)
                {
                    return TypedResults.BadRequest(result);
                }

                return TypedResults.Ok(result);
            })
            .RequireAuthorization("employee")
            .WithDescription("Bibliotekarz - potwierdzenie zwrócenia książek przez klienta na podstawie zeskanowanej fizycznie karty bibliotecznej (do testów z GetClients) oraz zeskanowanych ID kopii książek w formie listy.")
            .WithTags(Tags.Rentals);
    }
}