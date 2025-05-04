using Library.Domain.Books;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Books
{
    public class GetBooksByTitle : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("books/title/{title}", async Task<Results<Ok<List<Book>>, BadRequest<Result<List<Book>>>>> (string title, BookService bookService, CancellationToken cancalationToken) =>
            {
                var books = await bookService.GetBooksByTitle(title, cancalationToken);

                if (books.IsFailure)
                {
                    return TypedResults.BadRequest(books);
                }

                return TypedResults.Ok(books.Value);
            }).WithTags(Tags.Books);
        }
    }
}
