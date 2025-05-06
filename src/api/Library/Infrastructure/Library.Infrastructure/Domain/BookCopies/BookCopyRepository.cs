using Library.Domain.BookCopies.Interfaces;
using Library.Domain.BookCopies.Models;
using Library.Domain.SeedWork;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library.Infrastructure.Domain.BookCopies
{
    public class BookCopyRepository : IBookCopyPersistance
    {
        private readonly DbSet<BookCopy> bookCopyContext;

        public BookCopyRepository(LibraryContext context)
        {
            bookCopyContext = context.Set<BookCopy>();
        }

        public async Task<Result> AddBookCopyAsync(BookCopy book, CancellationToken cancellationToken)
        {
            var result = Result.Success();

            try
            {
                await bookCopyContext.AddAsync(book, cancellationToken);
            }
            catch (Exception ex)
            {
                result = Result.Failure(new Error("BookCopyRepository.AddBookCopyAsync", ex.Message));
            }

            return result;
        }

        public Result UpdateBookCopy(BookCopy book, CancellationToken cancellationToken)
        {
            var result = Result.Success();

            try
            {
                bookCopyContext.Update(book);
            }
            catch (Exception ex)
            {
                result = Result.Failure(new Error("BookCopyRepository.UpdateBookCopy", ex.Message));
            }

            return result;
        }

        public async Task<Result<List<Guid>>> IsAnyNonExistingBookCopyInGivenListAsync(List<Guid> bookCopyIds, CancellationToken cancellationToken)
        {
            Result<List<Guid>> result = default;

            try
            {
                var copies = bookCopyContext
                    .AsNoTracking()
                    .Where(x => bookCopyIds.Contains(x.Id))
                    .Select(x => x.Id)
                    .ToList();

                var nonExistingBookCopyIds = bookCopyIds.Except(copies).ToList();
                result = Result<List<Guid>>.Success(nonExistingBookCopyIds);

            }
            catch (Exception ex)
            {
                result = Result<List<Guid>>.Failure(new Error("BookCopyRepository.IsAnyNonExistingBookCopyInGivenListAsync", ex.Message));
            }

            return result;
        }

        public async Task<Result<BookCopy>> GetBookCopyByIdAsync(Guid bookId, CancellationToken cancellationToken)
        {
            Result<BookCopy> result = default;

            try
            {
                var bookCopy = await bookCopyContext.FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);
                result = Result<BookCopy>.Success(bookCopy);
            }
            catch (Exception ex)
            {
                result = Result<BookCopy>.Failure(new Error("BookRepository.GetByIdAsync", ex.Message));
            }

            return result;
        }

        public async Task<Result<Dictionary<Guid, List<Guid>>>> GetLocationIdsByBookId(List<Guid> bookIds, CancellationToken cancellationToken)
        {
            Result<Dictionary<Guid, List<Guid>>> result = default;

            try
            {              
                var bookLocations = await bookCopyContext
                    .AsNoTracking()
                    .Where(b => bookIds.Contains(b.BookId))
                    .GroupBy(x => x.BookId)
                    .ToDictionaryAsync(b => b.Key, l => l.Select(x => x.LocationId).ToList(), cancellationToken);

                result = Result<Dictionary<Guid, List<Guid>>>.Success(bookLocations);
            }
            catch(Exception ex)
            {
                result = Result<Dictionary<Guid, List<Guid>>>.Failure(new Error("BookCopyRepository.GetLocationIdsByBookId", ex.Message));
            }

            return result!;
        }

        public async Task<Result<Dictionary<Guid, List<Guid>>>> GetBookIdsByLocationId(Guid locationId, CancellationToken cancellationToken)
        {
            Result<Dictionary<Guid, List<Guid>>> result = default;

            try
            {
                var dictionary = await bookCopyContext
                    .AsNoTracking()
                    .Where(l => l.LocationId == locationId)
                    .GroupBy(x => x.LocationId)
                    .ToDictionaryAsync(l => l.Key, b => b.Select(x => x.BookId).ToList(), cancellationToken);
                result = Result<Dictionary<Guid, List<Guid>>>.Success(dictionary);
            }
            catch(Exception ex)
            {
                result = Result<Dictionary<Guid, List<Guid>>>.Failure(new Error("BookCopyRepository.GetBookIdsByLocationId", ex.Message));
            }

            return result!;
        }
    }
}
