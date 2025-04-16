using Library.Application.Book;
using Library.Domain.Books;

namespace Library.Api.Endpoints.Book
{
    public class ChangeBookGenre
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("books/changegenre", async ([FromBody] CreateBookDto createBookDto, BookService bookService, CancellationToken cancellationToken) =>
            {
                var result = await bookService.AddNewBookAsync(createBookDto.publisher,
                    createBookDto.title,
                    createBookDto.titlePageImageUrl,
                    createBookDto.releaseDate,
                    createBookDto.description,
                    createBookDto.ISBN,
                    Genre.FromName(createBookDto.genrenName),
                    cancellationToken);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result);
                }
                return Results.Created($"{createBookDto.ISBN}", createBookDto);
            })
                .WithTags(Tags.Books);
        }
    }
}
