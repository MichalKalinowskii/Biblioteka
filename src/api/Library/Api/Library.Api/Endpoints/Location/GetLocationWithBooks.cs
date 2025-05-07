
using Library.Application.Locations;
using Library.Domain.BookCopies;
using Library.Domain.Books;
using Library.Domain.Locations;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Location
{
    public class GetLocationWithBooks : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("location/{locationCode}",
                async Task<Results<Ok<LocationBooksDto>, BadRequest<Error>>> 
                    (string locationCode, LocationService locationService, BookCopyService bookCopyService, BookService bookService, CancellationToken cancalationToken) =>
            {
                var location = await locationService.GetLocationByCode(locationCode, cancalationToken);

                if (location.IsFailure)
                {
                    return TypedResults.BadRequest(location.Error);
                }

                var dictionary = await bookCopyService.GetBookIdsByLocationId(location.Value.Id, cancalationToken);

                if (dictionary.IsFailure)
                {
                    return TypedResults.BadRequest(dictionary.Error);
                }

                var resultBooks = await bookService.GetAllBooks(cancalationToken);
                if (resultBooks.IsFailure)
                {
                    return TypedResults.BadRequest(resultBooks.Error);
                }

                var books = resultBooks.Value
                    .Where(x => dictionary.Value.Values.SelectMany(y => y)
                        .Distinct()
                        .ToList()
                        .Contains(x.Id))
                    .ToList();

                var result = 
                    new LocationBooksDto(location.Value, books);
                
                return TypedResults.Ok(result);
            })
            .RequireAuthorization("client")
            .WithTags(Tags.Locations);
        }
    }
}
