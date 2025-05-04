using Library.Domain.Clients;
using Library.Domain.Rentals;

namespace Library.Api.Endpoints.Rentals;

public class GetRentals : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("rentals", async (Guid? clientId, IClientRepository clientRepository, IRentalRepository rentalRepository, CancellationToken cancellationToken) =>
            {
                Guid libraryCardId = Guid.Empty;
                
                if (clientId.HasValue)
                {
                    Client client = await clientRepository.GetByIdAsync((Guid)clientId, cancellationToken);

                    libraryCardId = client.LibraryCardId;
                }
                
                var rentals = await rentalRepository.GetAsync(libraryCardId, cancellationToken);
                
                return Results.Ok(rentals);
            })
            .RequireAuthorization("employee")
            .WithTags(Tags.Rentals);
    }
}