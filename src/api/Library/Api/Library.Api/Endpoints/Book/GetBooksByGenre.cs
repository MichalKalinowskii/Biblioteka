using Library.Domain.Books;
using Library.Domain.Books.Errors;
using Library.Domain.Books.Models;

namespace Library.Api.Endpoints.Book
{
    public class GetBooksByGenre : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("books/{genreName}", async (string genreName, BookService bookService, CancellationToken cancalationToken) =>
            {
                var genre = Genre.FromName(genreName);

                if (genre is null)
                {
                    return Results.BadRequest(BookErrors.InvalidaGenre);
                }

                var books = await bookService.GetAllBooksByGenreId(genre.Id, cancalationToken);

                if (books.IsFailure)
                {
                    return Results.BadRequest(books);
                }

                return Results.Ok(books.Value);
            }).WithTags(Tags.Books);
        }
    }
}
