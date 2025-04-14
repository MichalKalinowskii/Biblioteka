using Library.Domain.BookCopies.Interfaces;
using Library.Domain.BookCopies.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Domain.BookCopies
{
    public class BookCopyRepository : IBookCopyPersistance
    {
        public Task<Result> AddBookCopyAsync(BookCopy book, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateBookCopyAsync(BookCopy book, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Guid>>> IsAnyNonExistingBookCopyInGivenListAsync(List<Guid> bookIds, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<BookCopy> GetBookCopyByIdAsync(Guid bookId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
