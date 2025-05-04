using Dapper;
using Library.Application.Rentals;
using Library.Domain.Rentals;
using Library.Domain.SeedWork;
using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Rentals
{
    public class GetUserRentals : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("rentals/get-user-rentals", async Task<Results<Ok<IEnumerable<RentalDto>>, NotFound>> (
                    [FromServices] SqlConnectionFactory sqlConnectionFactory,
                    [FromServices] IAuthenticatedUserService authenticatedUserService,
                    CancellationToken cancellationToken) =>
                {
                    Guid? libraryCardId = authenticatedUserService.LibraryCardId;
                    
                    string sql = @"
                    SELECT 
                        rentals.""Id"" AS RentalId,
                        rentals.""RentalDate"" AS RentalDate,
                        rentals.""ReturnDate"" AS ReturnDate,
                        rentals.""Status"" AS Status,
                        bookCopies.""Id"" AS BookCopyId,
                        books.""Title"" AS BookTitle,
                        books.""Description"" AS BookDescription
                    FROM ""Rentals"" rentals
                    JOIN ""BookRentals"" bookRentals ON bookRentals.""RentalId"" = rentals.""Id""
                    JOIN ""BookCopies"" bookCopies ON bookCopies.""Id"" = bookRentals.""BookCopyId""
                    JOIN ""Books"" books ON bookCopies.""BookId"" = books.""Id""
                    WHERE rentals.""LibraryCardId"" = @LibraryCardId
                    AND rentals.""Status"" = ANY (@RentalStatuses)";
                    
                    int[] statuses = { (int)RentalStatus.InProgress, (int)RentalStatus.PartiallyReturned };
                    
                    var parameters = new { LibraryCardId = libraryCardId, RentalStatuses = statuses };

                    var connection = sqlConnectionFactory.GetOpenConnection();

                    var results = await connection.QueryAsync<RentalDto>(sql, parameters);

                    return results.Any() ? TypedResults.Ok(results) : TypedResults.NotFound();
                })
                .RequireAuthorization("client")
                .WithDescription("Client - pobieranie listy zalegających wypożyczeń.")
                .WithTags(Tags.Rentals);
        }
    }
}