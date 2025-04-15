using Library.Domain.Rentals;

namespace Library.Api.Endpoints.Rentals;

public class GetRentals : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("rentals", async (IRentalRepository rentalRepository, CancellationToken cancellationToken) =>
            {
                var rentals = await rentalRepository.GetAsync(cancellationToken);
                
                return Results.Ok(rentals);
            })
            .WithTags(Tags.Rentals);
    }
}