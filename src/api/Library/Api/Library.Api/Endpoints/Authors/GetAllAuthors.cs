
using Library.Domain.Authors;
using Library.Domain.Authors.Models;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Authors
{
    public class GetAllAuthors : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "authors", 
                async Task<Results<Ok<List<Author>>, BadRequest<Error>>> (AuthorService authorService, CancellationToken cancellationToken) =>
            {
                var result = await authorService.GetAllAuthorsAsync(cancellationToken);

                if (result.IsFailure)
                {
                    return TypedResults.BadRequest(result.Error);
                }

                return TypedResults.Ok(result.Value);

            })
            .RequireAuthorization()
            .WithTags(Tags.Authors);
        }
    }
}
