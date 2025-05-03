using Library.Application.Book;
using Library.Domain.BookCopies;
using Library.Domain.Books;
using Library.Domain.Locations;

namespace Library.Api.Endpoints.Book
{
    public class GetAllBooksWithLocation : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("books", 
                async (BookService bookService, LocationService locationService, BookCopyService bookCopyService, CancellationToken cancalationToken) =>
                {
                    var books = await bookService.GetAllBooks(cancalationToken);
                    if (books.IsFailure)
                    {
                        return Results.BadRequest(books);
                    }

                    var bookIdLocationIds = await bookCopyService
                        .GetLocationIdsByBookId(books.Value.Select(x => x.Id).ToList(), 
                        cancalationToken);
                    if (bookIdLocationIds.IsFailure)
                    {
                        return Results.BadRequest(bookIdLocationIds);
                    }

                    var locations = await locationService
                        .GetLocationByIds(bookIdLocationIds.Value.Values.SelectMany(x => x).Distinct().ToList(), cancalationToken);
                    if (locations.IsFailure)
                    {
                        return Results.BadRequest(locations);
                    }

                    var result = new List<GetBookLocationsDto>();

                    foreach (var bookLocationsValuePair in bookIdLocationIds.Value)
                    {
                        result.Add(new GetBookLocationsDto
                        {
                            Book = books.Value.FirstOrDefault(x => x.Id == bookLocationsValuePair.Key),
                            Locations = locations.Value.Where(x => bookLocationsValuePair.Value.Contains(x.Id)).ToList()
                        });
                    }

                    return Results.Ok(result);
                }).WithTags(Tags.Books);
        }
    }
}
