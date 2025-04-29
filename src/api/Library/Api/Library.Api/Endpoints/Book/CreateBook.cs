using Library.Application.Book;
using Library.Domain.Books;
using Library.Domain.Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Book
{
    public class CreateBook : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("book/add", async ([FromBody] CreateBookDto createBookDto, BookService bookService, CancellationToken cancellationToken) =>
                {
                    var result = await bookService.AddNewBookAsync(createBookDto.publisher,
                        createBookDto.title,
                        createBookDto.titlePageImageUrl,
                        createBookDto.releaseDate,
                        createBookDto.description,
                        createBookDto.ISBN,
                        Genre.FromName(createBookDto.genreName),
                        cancellationToken);

                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result);
                    }
                    return Results.Created();
                }).WithTags(Tags.Books);
        }
    }
}
