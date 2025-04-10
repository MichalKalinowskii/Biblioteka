using Library.Domain.Books.Entites;
using Library.Domain.Books.Interfaces;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;

namespace Library.Infrastructure.Domain.Books;

public class BookRepository : IBookPersistence
{
    public Task<Result> AddBookAsync(Book book, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Book> GetBookByISBN(string ISBN, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateBookAsync(Book book, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}