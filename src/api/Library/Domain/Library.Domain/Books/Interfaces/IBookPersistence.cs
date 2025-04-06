using Library.Domain.Books.Entites;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;

namespace Library.Domain.Books.Interfaces
{
    public interface IBookPersistence
    {
        Task<Result> AddBookAsync(Book book, CancellationToken cancellationToken);

        Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
