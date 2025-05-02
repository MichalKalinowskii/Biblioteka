using Library.Domain.Books;
using Library.Domain.Locations;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Book
{
    public class GetAllBooksWithLocation : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("books", async ([FromServices] BookService bookService, [FromServices] LocationService locationService, CancellationToken cancalationToken) =>
            {
                var books = await bookService.GetAllBooks(cancalationToken);
                if (books.IsFailure)
                {
                    return Results.BadRequest(books);
                }
                return Results.Ok(books.Value);
            }).WithTags(Tags.Books);
        }
    }
}
