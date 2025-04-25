using Library.Domain.Books;

namespace Library.Api.Endpoints.Book
{
    public class GetBooksByTitle : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("books/title/{title}", async (string title, BookService bookService, CancellationToken cancalationToken) =>
            {
                var books = await bookService.GetBooksByTitle(title, cancalationToken);

                if (books.IsFailure)
                {
                    return Results.BadRequest(books);
                }

                return Results.Ok(books.Value);
            }).WithTags(Tags.Books);
        }
    }
}
