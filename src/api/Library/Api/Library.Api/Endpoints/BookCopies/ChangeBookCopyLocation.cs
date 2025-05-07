using Library.Application.BookCopy;
using Library.Domain.BookCopies;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.BookCopies
{
    public class ChangeBookCopyLocation //: IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("bookcopy/changelocation", async Task<Results<Ok, BadRequest<Result>>> ([FromBody] BookCopyLocationDto bookCopyDto, BookCopyService bookService, CancellationToken cancellationToken) =>
            {
                var result = await bookService.ChangeBookCopyLocation(bookCopyDto.bookId,
                    bookCopyDto.LocationId,
                    cancellationToken);

                if (result.IsFailure)
                {
                    return TypedResults.BadRequest(result);
                }
                return TypedResults.Ok();
            }).WithTags(Tags.BookCopy);
        }
    }
}
