using Library.Application.Books;
using Library.Domain.Books;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.Books
{
    public class CreateBook //: IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("book/add", async Task<Results<Created, BadRequest<Result>>> ([FromBody] CreateBookDto createBookDto, BookService bookService, CancellationToken cancellationToken) =>
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
                        return TypedResults.BadRequest(result);
                    }
                    return TypedResults.Created();
                }).WithTags(Tags.Books);
        }
    }
}
