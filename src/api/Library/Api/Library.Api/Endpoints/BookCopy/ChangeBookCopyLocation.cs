using Library.Application.BookCopy;
using Library.Domain.BookCopies;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.BookCopy
{
    public class ChangeBookCopyLocation : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("bookcopy/status", async ([FromBody] BookCopyLocationDto bookCopyDto, BookCopyService bookService, CancellationToken cancellationToken) =>
            {
                var result = await bookService.ChangeBookCopyLocation(bookCopyDto.bookId,
                    bookCopyDto.LocationId,
                    cancellationToken);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result);
                }
                return Results.Ok();
            }).WithTags(Tags.BookCopy);
        }
    }
}
