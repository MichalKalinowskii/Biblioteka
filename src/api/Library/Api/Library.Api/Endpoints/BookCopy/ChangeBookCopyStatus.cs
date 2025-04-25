using Library.Application.BookCopy;
using Library.Domain.BookCopies;
using Library.Domain.BookCopies.Errors;
using Library.Domain.BookCopies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.BookCopy
{
    public class ChangeBookCopyStatus : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("bookcopy/status", async ([FromBody] BookCopyStatusDto bookCopyStatusDto, BookCopyService bookService, CancellationToken cancellationToken) =>
            {
                var bookCopyStatus = BookCopyStatus.FromName(bookCopyStatusDto.bookCopyStatusName);

                if (bookCopyStatus is null)
                {
                    return Results.BadRequest(BookCopyErrors.InvalidBookCopyStatusName);
                }

                var result = await bookService.ChangeBookCopyStatusAsync(bookCopyStatusDto.bookId,
                    bookCopyStatus,
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
