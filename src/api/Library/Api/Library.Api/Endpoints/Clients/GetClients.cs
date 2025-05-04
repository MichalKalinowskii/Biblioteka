using Dapper;
using Library.Application.Clients;
using Library.Application.Rentals;
using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Connections.Features;

namespace Library.Api.Endpoints.Clients;

public class GetClients : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("clients", async (SqlConnectionFactory sqlConnectionFactory) =>
        {
            var builder = new SqlBuilder();

            var template = builder.AddTemplate(@"
SELECT clients.""UserId"" AS Id, aspnetusers.""FirstName"" || aspnetusers.""LastName"" AS Name FROM ""Clients"" clients
JOIN ""AspNetUsers"" aspnetusers ON clients.""UserId"" = aspnetusers.""Id""
                                         ");
            
            var connection = sqlConnectionFactory.GetOpenConnection();

            var results = await connection.QueryAsync<ClientDto>(template.RawSql);

            return Results.Ok(results);
        })
            .RequireAuthorization("employee")
            .WithTags(Tags.Clients);
    }
}