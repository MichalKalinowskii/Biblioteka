using Library.Domain.Books;

namespace Library.Api.Endpoints.Book
{
    public class GetBooksByGenre : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("books/genre/{genreName}", async (string genreName, BookService bookService, CancellationToken cancalationToken) =>
            {
                var books = await bookService.GetAllBooksByGenreId(genreName, cancalationToken);

                if (books.IsFailure)
                {
                    return Results.BadRequest(books);
                }

                return Results.Ok(books.Value);
            }).WithTags(Tags.Books);
        }
    }
}
