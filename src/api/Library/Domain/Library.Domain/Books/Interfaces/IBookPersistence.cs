using Library.Domain.Books.Entites;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;

namespace Library.Domain.Books.Interfaces
{
    public interface IBookPersistence
    {
        Task<Result> AddBookAsync(Book book, CancellationToken cancellationToken);

        Task<Result<Book>> GetBookByISBN(string ISBN, CancellationToken cancellationToken);

        Task<Result<Book>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Result UpdateBook(Book book, CancellationToken cancellationToken);
    }
}
