
using Library.Domain.Books;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Books
{
    public class GetBookByISBN : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("book/isbn/{ISBN}", async Task<Results<Ok<Book>, BadRequest<Result<Book>>>> (string ISBN, BookService bookService, CancellationToken cancalationToken) =>
            {
                var book = await bookService.GetBookByISBN(ISBN, cancalationToken);

                if (book.IsFailure)
                {
                    return TypedResults.BadRequest(book);
                }

                return TypedResults.Ok(book.Value);
            })
            .RequireAuthorization()
            .WithTags(Tags.Books);
        }
    }
}
