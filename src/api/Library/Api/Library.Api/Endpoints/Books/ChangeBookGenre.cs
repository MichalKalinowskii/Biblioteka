using Library.Application.Books;
using Library.Domain.Books;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Books
{
    public class ChangeBookGenre : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("book/changegenre", async Task<Results<Ok<Result>, BadRequest<Result>>> ([FromBody] BookGenreDto bookGenreDto, BookService bookService, CancellationToken cancellationToken) =>
            {
                var result = await bookService.ChangeBookGenreAsync(bookGenreDto.ISBN,
                    Genre.FromName(bookGenreDto.genreName),
                    cancellationToken);
                    
                if (result.IsFailure)
                {
                    return TypedResults.BadRequest(result);
                }
                
                return TypedResults.Ok(result);
            }).WithTags(Tags.Books);
        }
    }
}
