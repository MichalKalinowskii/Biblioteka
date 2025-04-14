using Library.Domain.BookCopies.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.BookCopies.Interfaces
{
    public interface IBookCopyPersistance
    {
        Task<Result> AddBookCopyAsync(BookCopy book, CancellationToken cancellationToken);

        Task<Result> UpdateBookCopyAsync(BookCopy book, CancellationToken cancellationToken);

        Task<Result<List<Guid>>> IsAnyNonExistingBookCopyInGivenListAsync(List<Guid> bookIds, CancellationToken cancellationToken);

        Task<BookCopy> GetBookCopyByIdAsync(Guid bookId, CancellationToken cancellationToken);
    }
}
