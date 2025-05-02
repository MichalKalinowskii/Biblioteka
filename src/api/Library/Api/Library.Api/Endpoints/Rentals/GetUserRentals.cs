
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
            app.MapGet("rentals/get-user-rentals", async ([FromServices] SqlConnectionFactory sqlConnectionFactory, IAuthenticatedUserService authenticatedUserService, CancellationToken cancellationToken) =>
            {
                var connection = sqlConnectionFactory.GetOpenConnection();
                Guid? userId = authenticatedUserService.UserId;

                return Results.Ok();
            })
                .RequireAuthorization()
                .WithTags(Tags.Rentals);
        }
    }
}
