using Dapper;
using Library.Application.BookCopy;
using Library.Application.Rentals;
using Library.Domain.Clients;
using Library.Domain.Rentals;
using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Rentals;

public class GetActiveRentals : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("rentals/get-active-rentals", async Task<Results<Ok<List<RentalDto>>, NotFound>> (Guid? clientId, IClientRepository clientRepository, IRentalRepository rentalRepository, SqlConnectionFactory sqlConnectionFactory, CancellationToken cancellationToken) =>
            {
                Guid libraryCardId = Guid.Empty;
                
                if (clientId.HasValue)
                {
                    Client client = await clientRepository.GetByIdAsync((Guid)clientId, cancellationToken);

                    libraryCardId = client.LibraryCardId;
                }
                
                var builder = new SqlBuilder();

                var template = builder.AddTemplate(@"
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
                                          /**where**/
                                         ");
                
                int[] statuses = { (int)RentalStatus.InProgress, (int)RentalStatus.PartiallyReturned };
                    
                builder.Where(@"rentals.""Status"" = ANY (@RentalStatuses)", new {RentalStatuses = statuses});

                if (libraryCardId != Guid.Empty)
                {
                    builder.Where(@"rentals.""LibraryCardId"" = @LibraryCardId", new { LibraryCardId = libraryCardId });
                }
                
                var connection = sqlConnectionFactory.GetOpenConnection();

                var rentalDict = new Dictionary<int, RentalDto>();
                
                var results = await connection.QueryAsync<RentalDto, BookCopyDto, RentalDto>(template.RawSql,
                    (rental, bookCopy) =>
                    {
                        if (!rentalDict.TryGetValue(rental.RentalId, out var existingRental))
                        {
                            existingRental = rental;
                            existingRental.BookCopies = new List<BookCopyDto>();
                            rentalDict.Add(rental.RentalId, existingRental);
                        }

                        if (!existingRental.BookCopies.Any(b => b.Id == bookCopy.Id))
                        {
                            existingRental.BookCopies.Add(bookCopy);
                        }

                        return existingRental;
                    }, template.Parameters, splitOn: "BookCopyId");
                
                return rentalDict.Values.Any() ?  TypedResults.Ok(rentalDict.Values.ToList()) : TypedResults.NotFound();
            })
            .RequireAuthorization("employee")
            .WithDescription("Bibliotekarz - pobieranie zalegających wypożyczeń, możliwa filtracja po ID Clienta (GetClients).")
            .WithTags(Tags.Rentals);
    }
}