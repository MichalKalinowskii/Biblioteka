using Library.Domain.Clients;
using Library.Domain.Rentals;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Rentals;

public class GetRentals : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("rentals", async Task<Results<Ok<List<Rental>>, NotFound>> (Guid? clientId, IClientRepository clientRepository, IRentalRepository rentalRepository, CancellationToken cancellationToken) =>
            {
                Guid libraryCardId = Guid.Empty;
                
                if (clientId.HasValue)
                {
                    Client client = await clientRepository.GetByIdAsync((Guid)clientId, cancellationToken);

                    libraryCardId = client.LibraryCardId;
                }
                
                var rentals = await rentalRepository.GetActiveRentalsAsync(libraryCardId, cancellationToken);
                
                return rentals.Any() ?  TypedResults.Ok(rentals) : TypedResults.NotFound();
            })
            .RequireAuthorization("employee")
            .WithDescription("Bibliotekarz - pobieranie zalegających wypożyczeń, możliwa filtracja po ID Clienta (GetClients).")
            .WithTags(Tags.Rentals);
    }
}