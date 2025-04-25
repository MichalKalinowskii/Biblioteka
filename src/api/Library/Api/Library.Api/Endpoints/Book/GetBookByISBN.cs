
using Library.Domain.Books;

namespace Library.Api.Endpoints.Book
{
    public class GetBookByISBN : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("book/{ISBN}", async (string ISBN, BookService bookService, CancellationToken cancalationToken) =>
            {
                var book = await bookService.GetBookByISBN(ISBN, cancalationToken);

                if (book.IsFailure)
                {
                    return Results.BadRequest(book);
                }

                return Results.Ok(book.Value);
            }).WithTags(Tags.Books);
        }
    }
}
