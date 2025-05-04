using Dapper;
using Library.Application.Clients;
using Library.Application.Rentals;
using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Clients;

public class GetClients : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("clients", async Task<Results<Ok<IEnumerable<ClientDto>>, NotFound>> (SqlConnectionFactory sqlConnectionFactory) =>
        {
            var builder = new SqlBuilder();

            var template = builder.AddTemplate(@"
SELECT clients.""UserId"" AS Id, clients.""LibraryCardId"", aspnetusers.""FirstName"" || aspnetusers.""LastName"" AS Name FROM ""Clients"" clients
JOIN ""AspNetUsers"" aspnetusers ON clients.""UserId"" = aspnetusers.""Id""
                                         ");
            
            var connection = sqlConnectionFactory.GetOpenConnection();

            var results = await connection.QueryAsync<ClientDto>(template.RawSql);

            return results.Any() ? TypedResults.Ok(results) : TypedResults.NotFound();
        })
            .RequireAuthorization("employee")
            .WithDescription("Bibliotekarz - pobieranie listy klientów")
            .WithTags(Tags.Clients);
    }
}