using Library.Domain.Books.Entites;
using Library.Domain.Books.Errors;
using Library.Domain.Books.Interfaces;
using Library.Domain.Books.Models;
using Library.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Books
{
    public class BookService
    {
        private readonly IBookPersistence bookPersistence;
        private readonly IUnitOfWork unitOfWork;

        public BookService(IBookPersistence bookPersistence, IUnitOfWork unitOfWork)
        {
            this.bookPersistence = bookPersistence;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> AddNewBookAsync(BookEntity book, CancellationToken cancellationToken)
        {
            var result = BookFactory.Create(book);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            var newBook = result.Value!;

            var bookAlreadyExist = bookPersistence.GetBookByISBN(newBook.ISBN, cancellationToken) is not null;
            
            if (bookAlreadyExist)
            {
                return Result.Failure(BookErrors.BookAlreadyExists);
            }

            var addBookResult = await bookPersistence.AddBookAsync(newBook, cancellationToken);

            if (addBookResult.IsFailure)
            {
                return Result.Failure(addBookResult.Error);
            }

            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> ChangeBookGenreAsync(string ISBN, string newGenreName, CancellationToken cancellationToken)
        {
            var book = await bookPersistence.GetBookByISBN(ISBN, cancellationToken);
            
            if (book is null)
            {
                return Result.Failure(BookErrors.BookAlreadyExists);
            }

            book.ChangeGenre(newGenreName);

            var updateBookResult = await bookPersistence.UpdateBookAsync(book, cancellationToken);
            if (updateBookResult.IsFailure)
            {
                return Result.Failure(updateBookResult.Error);
            }
            
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }

    }
}
