using Library.Application.Books;
using Library.Domain.BookCopies;
using Library.Domain.Books;
using Library.Domain.Books.Models;
using Library.Domain.Locations;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Books
{    public class GetAllBooksWithLocation : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("books", 
                async Task<Results<Ok<List<GetBookLocationsDto>>, BadRequest<Error>>> (BookService bookService, LocationService locationService, BookCopyService bookCopyService, CancellationToken cancalationToken) =>
                {
                    var books = await bookService.GetAllBooks(cancalationToken);
                    if (books.IsFailure)
                    {
                        return TypedResults.BadRequest(books.Error);
                    }

                    var bookIdLocationIds = await bookCopyService
                        .GetLocationIdsByBookId(books.Value.Select(x => x.Id).ToList(), 
                            cancalationToken);
                    if (bookIdLocationIds.IsFailure)
                    {
                        return TypedResults.BadRequest(bookIdLocationIds.Error);
                    }

                    var locations = await locationService
                        .GetLocationByIds(bookIdLocationIds.Value.Values.SelectMany(x => x).Distinct().ToList(), cancalationToken);
                    if (locations.IsFailure)
                    {
                        return TypedResults.BadRequest(locations.Error);
                    }

                    var result = new List<GetBookLocationsDto>();

                    foreach (var bookLocationsValuePair in bookIdLocationIds.Value)
                    {
                        result.Add(
                        new GetBookLocationsDto(
                            books.Value.FirstOrDefault(x => x.Id == bookLocationsValuePair.Key),
                            locations.Value.Where(x => bookLocationsValuePair.Value.Contains(x.Id)).ToList()));
                    }

                    return TypedResults.Ok(result);
                })
                .RequireAuthorization("client")
                .WithTags(Tags.Books);
        }
    }
}
