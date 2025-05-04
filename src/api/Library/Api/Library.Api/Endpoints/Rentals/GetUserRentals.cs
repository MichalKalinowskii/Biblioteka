using Dapper;
using Library.Application.Rentals;
using Library.Domain.Rentals;
using Library.Domain.SeedWork;
using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Rentals
{
    public class GetUserRentals : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("rentals/get-user-rentals", async ([FromQuery] int[] rentalStatuses,
                    [FromServices] SqlConnectionFactory sqlConnectionFactory,
                    [FromServices] IAuthenticatedUserService authenticatedUserService, CancellationToken cancellationToken) =>
                {
                    Guid? libraryCardId = authenticatedUserService.LibraryCardId;

                    var parameters = new
                    {
                        LibraryCardId = libraryCardId,
                        RentalStatuses = rentalStatuses
                    };

                    var builder = new SqlBuilder();

                    var template = builder.AddTemplate(@"
                                          SELECT 
                                        rentals.""Id"" AS RentalId,
                                        rentals.""RentalDate"" AS RentalDate,
                                        rentals.""ReturnDate"" AS ReturnDate,
                                        bookCopies.""Id"" AS BookCopyId,
                                        books.""Title"" AS BookTitle,
                                        books.""Description"" AS BookDescription
                                        FROM ""Rentals"" rentals
                                        JOIN ""BookRentals"" bookRentals ON bookRentals.""RentalId"" = rentals.""Id""
                                        JOIN ""BookCopies"" bookCopies ON bookCopies.""Id"" = bookRentals.""BookCopyId""
                                        JOIN ""Books"" books ON bookCopies.""BookId"" = books.""Id""
                                          /**where**/
                                         ");


                    builder.Where(@"rentals.""LibraryCardId"" = @LibraryCardId", new { LibraryCardId = libraryCardId });
                    
                    if (rentalStatuses.Any())
                    {
                        builder.Where(@"rentals.""Status"" IN @RentalStatuses", new { RentalStatuses = rentalStatuses });
                    }

                    var connection = sqlConnectionFactory.GetOpenConnection();

                    var results = await connection.QueryAsync<RentalDto>(template.RawSql, parameters);

                    return Results.Ok(results);
                })
                .RequireAuthorization("client")
                .WithTags(Tags.Rentals);
        }
    }
}