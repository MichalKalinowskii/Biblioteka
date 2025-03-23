using Library.Domain.Books.Entites;
using Library.Domain.Books.Interfaces;
using Library.Domain.SeedWork;

namespace Library.Infrastructure.Domain.Books;

public class BookRepository : IBookPersistence
{
    public Task<Result> Save(BookEntity book)
    {
        throw new NotImplementedException();
    }
}