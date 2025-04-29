using Library.Application.BookCopy;
using Library.Domain.BookCopies;
using Library.Domain.BookCopies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.BookCopy
{
    public class AddBookCopy : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("bookcopy/add", async ([FromBody] BookCopyAddNewDto bookCopyDto, BookCopyService bookService, CancellationToken cancellationToken) =>
            {
                var result = await bookService.AddNewBookCopyAsync(bookCopyDto.BookId,
                    bookCopyDto.LocationId,
                    BookCopyStatus.FromName(bookCopyDto.statusName),
                    cancellationToken);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result);
                }
                return Results.Created();
            }).WithTags(Tags.BookCopy);
        }
    }
}
