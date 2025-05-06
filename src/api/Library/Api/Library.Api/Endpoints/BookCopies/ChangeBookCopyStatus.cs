using Library.Application.BookCopy;
using Library.Domain.BookCopies;
using Library.Domain.BookCopies.Models;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.BookCopies
{
    public class ChangeBookCopyStatus //: IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("bookcopy/changestatus", async Task<Results<Ok, BadRequest<Result>>> ([FromBody] BookCopyStatusDto bookCopyStatusDto, BookCopyService bookService, CancellationToken cancellationToken) =>
            {
                var bookCopyStatus = BookCopyStatus.FromName(bookCopyStatusDto.bookCopyStatusName);

                var result = await bookService.ChangeBookCopyStatusAsync(bookCopyStatusDto.bookId,
                    bookCopyStatus,
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
