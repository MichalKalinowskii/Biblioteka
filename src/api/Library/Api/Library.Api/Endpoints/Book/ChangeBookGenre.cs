using Library.Application.Book;
using Library.Domain.Books;
using Library.Domain.Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Book
{
    public class ChangeBookGenre : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("books/changegenre", async ([FromBody] BookGenreDto bookGenreDto, BookService bookService, CancellationToken cancellationToken) =>
            {
                var result = await bookService.ChangeBookGenreAsync(bookGenreDto.ISBN,
                    Genre.FromName(bookGenreDto.genreName),
                    cancellationToken);
                    
                if (result.IsFailure)
                {
                    return Results.BadRequest(result);
                }
                return Results.Ok();
            }).WithTags(Tags.Books);
        }
    }
}
