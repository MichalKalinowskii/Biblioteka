
using Library.Domain.Locations;
using Library.Domain.Locations.Models;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Location
{
    public class GetLocations : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "locations",
                async Task<Results<Ok<List<Library.Domain.Locations.Models.Location>>, BadRequest<Error>>> (LocationService locationService) =>
                {
                    var locations = await locationService.GetAllLocationsAsync(CancellationToken.None);
                    if (locations.IsFailure)
                    {
                        return TypedResults.BadRequest(locations.Error);
                    }

                    return TypedResults.Ok(locations.Value);

                })
                .RequireAuthorization()
                .WithTags(Tags.Locations);
        }
    }
}
