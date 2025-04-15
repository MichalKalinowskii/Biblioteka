using Library.Domain.BookCopies.Errors;
using Library.Domain.BookCopies.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.BookCopies
{
    public class BookCopyFactory
    {
        private BookCopyFactory() { }

        public static Result<BookCopy> Create(Guid bookId,
            Guid locationId,
            BookCopyStatus bookCopyStatus)
        {
            if (bookId == Guid.Empty)
            {
                return Result<BookCopy>.Failure(BookCopyErrors.BookIdMissing);
            }

            if (locationId == Guid.Empty)
            {
                return Result<BookCopy>.Failure(BookCopyErrors.LocationIdMissing);
            }

            if (bookCopyStatus is null)
            {
                return Result<BookCopy>.Failure(BookCopyErrors.BookCopyStatusMissing);
            }

            var bookCopy = new BookCopy(Guid.NewGuid(),
                bookId,
                locationId,
                bookCopyStatus);

            return Result<BookCopy>.Success(bookCopy);
        }
    }
}
