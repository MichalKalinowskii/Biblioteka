using Library.Domain.Books;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Books
{
    public class GetBooksByGenre : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("books/genre/{genreName}", async Task<Results<Ok<List<Book>>, BadRequest<Result<List<Book>>>>> (string genreName, BookService bookService, CancellationToken cancalationToken) =>
            {
                var books = await bookService.GetAllBooksByGenreId(genreName, cancalationToken);

                if (books.IsFailure)
                {
                    return TypedResults.BadRequest(books);
                }

                return TypedResults.Ok(books.Value);
            })
            .RequireAuthorization()
            .WithTags(Tags.Books);
        }
    }
}
