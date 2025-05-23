﻿using Library.Domain.BookCopies.Models;
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

        Result UpdateBookCopy(BookCopy book, CancellationToken cancellationToken);

        Task<Result<List<Guid>>> IsAnyNonExistingBookCopyInGivenListAsync(List<Guid> bookIds, CancellationToken cancellationToken);

        Task<Result<BookCopy>> GetBookCopyByIdAsync(Guid bookId, CancellationToken cancellationToken);

        Task<Result<Dictionary<Guid, List<Guid>>>> GetLocationIdsByBookId(List<Guid> bookIds, CancellationToken cancellationToken);
        
        Task<Result<Dictionary<Guid, List<Guid>>>> GetBookIdsByLocationId(Guid locationId, CancellationToken cancellationToken);

        Task<List<Guid>> UnavailableBookCopyIds(List<Guid> bookCopyIds, CancellationToken cancellationToken);

        Task SetBookCopiesStatus(List<Guid> bookCopyIds, BookCopyStatus status, CancellationToken cancellationToken);
    }
}
