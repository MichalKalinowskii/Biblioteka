using Library.Application.BookCopy;
using Library.Domain.BookCopies;
using Library.Domain.BookCopies.Models;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints.BookCopies
{
    public class AddBookCopy //: IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("bookcopy/add", async Task<Results<Created, BadRequest<Result<BookCopy>>>> ([FromBody] BookCopyAddNewDto bookCopyDto, BookCopyService bookService, CancellationToken cancellationToken) =>
            {
                var result = await bookService.AddNewBookCopyAsync(bookCopyDto.BookId,
                    bookCopyDto.LocationId,
                    BookCopyStatus.FromName(bookCopyDto.statusName),
                    cancellationToken);

                if (result.IsFailure)
                {
                    return TypedResults.BadRequest(result);
                }
                return TypedResults.Created();
            }).WithTags(Tags.BookCopy);
        }
    }
}
