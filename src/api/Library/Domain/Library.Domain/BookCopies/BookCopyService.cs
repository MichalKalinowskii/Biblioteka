using Library.Domain.BookCopies.Errors;
using Library.Domain.BookCopies.Interfaces;
using Library.Domain.BookCopies.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Books.Models;

namespace Library.Domain.BookCopies
{
    public class BookCopyService
    {
        private readonly IBookCopyPersistance bookCopyPersistance;
        private readonly IUnitOfWork unitOfWork;

        public BookCopyService(IBookCopyPersistance bookCopyPersistance, IUnitOfWork unitOfWork)
        {
            this.bookCopyPersistance = bookCopyPersistance;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<BookCopy>> AddNewBookCopyAsync(Guid BookId,
            Guid LocationId,
            BookCopyStatus Status,
            CancellationToken cancellationToken)
        {
            var bookCopy = BookCopyFactory.Create(BookId, LocationId, Status);

            if (bookCopy.IsFailure)
            {
                return Result<BookCopy>.Failure(bookCopy.Error);
            }

            var addBookCopyResult = await bookCopyPersistance.AddBookCopyAsync(bookCopy.Value!, cancellationToken);
            if (addBookCopyResult.IsFailure)
            {
                return Result<BookCopy>.Failure(addBookCopyResult.Error);
            }

            await unitOfWork.CommitAsync(cancellationToken);
            return Result<BookCopy>.Success(bookCopy.Value!);
        }

        public async Task<Result> ChangeBookCopyStatusAsync(Guid bookCopyId,
            BookCopyStatus newStatus,
            CancellationToken cancellationToken)
        {
            var bookCopy = await bookCopyPersistance.GetBookCopyByIdAsync(bookCopyId, cancellationToken);

            if (bookCopy.IsFailure)
            {
                return Result.Failure(bookCopy.Error);
            }

            if (bookCopy.Value is null)
            {
                return Result.Failure(BookCopyErrors.BookCopyNotFound);
            }

            var statusResult = bookCopy.Value.ChangeStatus(newStatus);
            if (statusResult.IsFailure)
            {
                return Result.Failure(statusResult.Error);
            }

            var updateResult = bookCopyPersistance.UpdateBookCopy(bookCopy.Value, cancellationToken);

            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            await unitOfWork.CommitAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<List<Guid>>> IsAnyNonExistingBookCopyInGivenListAsync(List<Guid> bookCopyIds, CancellationToken cancellationToken)
        {
            var result = await bookCopyPersistance.IsAnyNonExistingBookCopyInGivenListAsync(bookCopyIds, cancellationToken);

            if (result.IsFailure)
            {
                return Result<List<Guid>>.Failure(result.Error);
            }

            return Result<List<Guid>>.Success(result.Value!);
        }

        public async Task<Result> ChangeBookCopyLocation(Guid bookId,
            Guid locationId,
            CancellationToken cancellationToken)
        {
            var bookCopy = await bookCopyPersistance.GetBookCopyByIdAsync(bookId, cancellationToken);

            if (bookCopy.IsFailure)
            {
                return Result.Failure(bookCopy.Error);
            }

            if (bookCopy.Value is null)
            {
                return Result.Failure(BookCopyErrors.BookCopyNotFound);
            }

            var result = bookCopy.Value.ChangeLocation(locationId);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            var updateResult = bookCopyPersistance.UpdateBookCopy(bookCopy.Value, cancellationToken);

            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            await unitOfWork.CommitAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<Dictionary<Guid, List<Guid>>>> GetLocationIdsByBookId(List<Guid> bookIds, CancellationToken cancellationToken)
        {
            var result = await bookCopyPersistance.GetLocationIdsByBookId(bookIds, cancellationToken);

            if (result.IsFailure)
            {
                return Result<Dictionary<Guid, List<Guid>>>.Failure(result.Error);
            }

            return Result<Dictionary<Guid, List<Guid>>>.Success(result.Value!);
        }

        public async Task<Result<Dictionary<Guid, List<Guid>>>> GetBookIdsByLocationId(Guid locationId, CancellationToken cancellationToken)
        {
            var result = await bookCopyPersistance.GetBookIdsByLocationId(locationId, cancellationToken);
            if (result.IsFailure)
            {
                return Result<Dictionary<Guid, List<Guid>>>.Failure(result.Error);
            }

            return Result<Dictionary<Guid, List<Guid>>>.Success(result.Value);
        }
    }
}
