using Library.Api.Endpoints.Books;
using Library.Application.Authors;
using Library.Domain.Authors;
using Library.Domain.Books;
using Library.Domain.SeedWork;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Api.Endpoints.Authors
{
    public class GetAuthorBook : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("authors/{bookTitle}", 
                async Task<Results<Ok<AuthorBookDto>, NotFound>> (string bookTitle, BookService bookService, AuthorService authorService, CancellationToken cancellationToken) =>
            {
                var books = await bookService.GetBooksByTitle(bookTitle, cancellationToken);

                if (books.IsFailure)
                {
                    return TypedResults.NotFound();
                }

                var book = books.Value.FirstOrDefault();
                if (book is null)
                {
                    return TypedResults.NotFound();
                }

                var authors = await authorService.GetAuthorBookAsync(book.Id, cancellationToken);
                if (authors.IsFailure)
                {
                    return TypedResults.NotFound();
                }

                var result = new AuthorBookDto()
                {
                    Book = book,
                    Authors = authors.Value
                };

                return TypedResults.Ok(result);
            })
            .RequireAuthorization("client")
            .WithTags(Tags.Authors);
        }
    }
}
