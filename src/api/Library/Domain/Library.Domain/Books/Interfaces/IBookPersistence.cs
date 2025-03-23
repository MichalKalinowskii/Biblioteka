using Library.Domain.Books.Entites;
using Library.Domain.SeedWork;

namespace Library.Domain.Books.Interfaces
{
    public interface IBookPersistence
    {
        Task<Result> Save(BookEntity book);
    }
}
